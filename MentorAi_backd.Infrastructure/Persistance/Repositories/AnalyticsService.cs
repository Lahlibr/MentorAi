// ConsoleAnalytics/Services/AnalyticsService.cs
using Microsoft.EntityFrameworkCore;
using System.Text;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.Extensions.Logging;

namespace CodingPlatform.ConsoleAnalytics.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly MentorAiDbContext _context;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(MentorAiDbContext context , ILogger<AnalyticsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task GenerateStudentPerformanceReport()
        {
           _logger.LogInformation("Generating student performance report...");

            var submissions = await _context.Submissions
                .Include(s => s.Problem)
                .Include(s => s.Student)
                .Include(s => s.TestCaseResults)
                .OrderBy(s => s.StudentId)
                .ThenBy(s => s.ProblemId)
                .ThenBy(s => s.SubmissionTime)
                .ToListAsync();

            if (!submissions.Any())
            {
               _logger.LogWarning("No submissions found for generating the report.");
                return;
            }

            var studentProblemGroups = submissions
                .GroupBy(s => new { s.StudentId, s.ProblemId })
                .Select(g => new
                {
                    StudentId = g.Key.StudentId,
                    StudentName = g.First().Student.User.UserName,
                    ProblemId = g.Key.ProblemId,
                    ProblemTitle = g.First().Problem.Title,
                    Submissions = g.OrderBy(s => s.SubmissionTime).ToList()
                })
                .OrderBy(spg => spg.StudentId)
                .ThenBy(spg => spg.ProblemId)
                .ToList();

            _logger.LogInformation($"Found {studentProblemGroups.Count} student-problem groups for report generation.");

            foreach (var spg in studentProblemGroups)
            {
                _logger.LogInformation($"Processing submissions for Student ID: {spg.StudentId}, Problem ID: {spg.ProblemId}");
                _logger.LogInformation($"Problem: {spg.ProblemTitle} (ID: {spg.ProblemId})");
                _logger.LogInformation(new string('-', 50));

                var firstSubmission = spg.Submissions.First();
                var firstCorrectSubmission = spg.Submissions.FirstOrDefault(s => s.IsCorrect);
                var lastSubmission = spg.Submissions.Last();

                TimeSpan timeTaken;
                string status;
                int wrongSubmissionsCount;

                if (firstCorrectSubmission != null)
                {
                    timeTaken = firstCorrectSubmission.SubmissionTime - firstSubmission.ProblemStartTime;
                    status = "✅ SOLVED";
                    wrongSubmissionsCount = spg.Submissions
                        .Count(s => !s.IsCorrect && s.SubmissionTime < firstCorrectSubmission.SubmissionTime);
                }
                else
                {
                    timeTaken = lastSubmission.SubmissionTime - firstSubmission.ProblemStartTime;
                    status = "❌ UNSOLVED";
                    wrongSubmissionsCount = spg.Submissions.Count;
                }

                _logger.LogInformation($"  Status: {status}");
                _logger.LogInformation($"  Time Taken: {timeTaken.TotalMinutes:F2} minutes");
                _logger.LogInformation($"  Total Attempts: {spg.Submissions.Count}");
                _logger.LogInformation($"  Wrong Submissions: {wrongSubmissionsCount}");

                if (firstCorrectSubmission == null)
                {
                    var failedTestCases = lastSubmission.TestCaseResults?.Count(tcr => !tcr.Passed) ?? 0;
                    _logger.LogInformation($"  Failed Test Cases (Last Attempt): {failedTestCases}");

                    if (!string.IsNullOrEmpty(lastSubmission.CompileError))
                    {
                        _logger.LogInformation($"  Compile Error: {lastSubmission.CompileError}");
                    }
                }

                Console.WriteLine();
            }

            // Summary statistics
            var totalStudents = submissions.Select(s => s.StudentId).Distinct().Count();
            var totalProblems = submissions.Select(s => s.ProblemId).Distinct().Count();
            var solvedSubmissions = submissions.Where(s => s.IsCorrect).ToList();
            var avgSolveTime = solvedSubmissions.Any()
                ? solvedSubmissions.Average(s => (s.SubmissionTime - s.ProblemStartTime).TotalMinutes)
                : 0;

            _logger.LogInformation("=== SUMMARY STATISTICS ===");
            _logger.LogInformation($"Total Students: {totalStudents}");
            _logger.LogInformation($"Total Problems: {totalProblems}");
            _logger.LogInformation($"Total Submissions: {submissions.Count}");
            _logger.LogInformation($"Successful Submissions: {solvedSubmissions.Count}");
            _logger.LogInformation($"Average Solve Time: {avgSolveTime:F2} minutes");
        }

        public async Task ExportAllAnalyticsToCsv()
        {
            Console.WriteLine("Exporting analytics to CSV...\n");

            var submissions = await _context.Submissions
                .Include(s => s.Problem)
                .Include(s => s.Student)
                .Include(s => s.TestCaseResults)
                .ToListAsync();

            var studentProblemGroups = submissions
                .GroupBy(s => new { s.StudentId, s.ProblemId })
                .Select(g => new
                {
                    StudentId = g.Key.StudentId,
                    StudentName = g.First().Student.User.UserName,
                    ProblemId = g.Key.ProblemId,
                    ProblemTitle = g.First().Problem.Title,
                    Submissions = g.OrderBy(s => s.SubmissionTime).ToList()
                })
                .ToList();

            var csv = new StringBuilder();
            csv.AppendLine("StudentID,StudentName,ProblemID,ProblemTitle,Status,TotalAttempts,TimeTaken(Minutes),WrongSubmissions,FirstAttemptTime,SolvedTime");

            foreach (var spg in studentProblemGroups)
            {
                var firstSubmission = spg.Submissions.First();
                var firstCorrectSubmission = spg.Submissions.FirstOrDefault(s => s.IsCorrect);

                var status = firstCorrectSubmission != null ? "Solved" : "Unsolved";
                var timeTaken = firstCorrectSubmission != null
                    ? (firstCorrectSubmission.SubmissionTime - firstSubmission.ProblemStartTime).TotalMinutes
                    : (spg.Submissions.Last().SubmissionTime - firstSubmission.ProblemStartTime).TotalMinutes;
                var wrongSubmissions = firstCorrectSubmission != null
                    ? spg.Submissions.Count(s => !s.IsCorrect && s.SubmissionTime < firstCorrectSubmission.SubmissionTime)
                    : spg.Submissions.Count;

                csv.AppendLine($"{spg.StudentId}," +
                              $"\"{spg.StudentName}\"," +
                              $"{spg.ProblemId}," +
                              $"\"{spg.ProblemTitle}\"," +
                              $"{status}," +
                              $"{spg.Submissions.Count}," +
                              $"{timeTaken:F2}," +
                              $"{wrongSubmissions}," +
                              $"{firstSubmission.SubmissionTime:yyyy-MM-dd HH:mm:ss}," +
                              $"{(firstCorrectSubmission?.SubmissionTime.ToString("yyyy-MM-dd HH:mm:ss") ?? "")}");
            }

            var fileName = $"analytics_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            await File.WriteAllTextAsync(fileName, csv.ToString());

            Console.WriteLine($"Analytics exported to: {fileName}");
            Console.WriteLine($"Total records: {studentProblemGroups.Count}");
        }

        public async Task ShowProblemStatistics()
        {
            Console.WriteLine("=== PROBLEM STATISTICS ===\n");

            var problems = await _context.Problems
                .Include(p => p.Submission)
                .ThenInclude(s => s.Student)
                .ToListAsync();

            if (!problems.Any())
            {
                Console.WriteLine("No problems found.");
                return;
            }

            foreach (var problem in problems.OrderBy(p => p.Id))
            {
                var submissions = problem.Submission.ToList();
                var students = submissions.Select(s => s.StudentId).Distinct().Count();
                var solvedSubmissions = submissions.Where(s => s.IsCorrect).ToList();
                var uniqueSolvers = solvedSubmissions.Select(s => s.StudentId).Distinct().Count();

                var solveRate = students > 0 ? (double)uniqueSolvers / students * 100 : 0;
                var avgAttempts = students > 0
                    ? submissions.GroupBy(s => s.StudentId).Average(g => g.Count())
                    : 0;
                var avgSolveTime = solvedSubmissions.Any()
                    ? solvedSubmissions.Average(s => (s.SubmissionTime - s.ProblemStartTime).TotalMinutes)
                    : 0;

                Console.WriteLine($"Problem: {problem.Title} ({problem.DifficultyLevel})");
                Console.WriteLine($"  Total Students Attempted: {students}");
                Console.WriteLine($"  Students Solved: {uniqueSolvers}");
                Console.WriteLine($"  Solve Rate: {solveRate:F1}%");
                Console.WriteLine($"  Total Submissions: {submissions.Count}");
                Console.WriteLine($"  Average Attempts per Student: {avgAttempts:F1}");
                Console.WriteLine($"  Average Solve Time: {avgSolveTime:F2} minutes");
                Console.WriteLine();
            }
        }

        public async Task ShowLeaderboard()
        {
            Console.WriteLine("=== LEADERBOARD ===\n");

            var students = await _context.StudentProfiles
                .Include(s => s.Submissions)
                .ThenInclude(sub => sub.Problem)
                .ToListAsync();

            var leaderboardData = students.Select(student =>
            {
                var submissions = student.Submissions.ToList();
                var problemGroups = submissions.GroupBy(s => s.ProblemId).ToList();
                var solvedProblems = problemGroups.Where(g => g.Any(s => s.IsCorrect)).ToList();

                var avgTime = 0.0;
                if (solvedProblems.Any())
                {
                    var times = solvedProblems.Select(g =>
                    {
                        var firstCorrect = g.First(s => s.IsCorrect);
                        var firstAttempt = g.OrderBy(s => s.SubmissionTime).First();
                        return (firstCorrect.SubmissionTime - firstAttempt.ProblemStartTime).TotalMinutes;
                    });
                    avgTime = times.Average();
                }

                return new
                {
                    StudentId = student.UserId,
                    StudentName = student.User.UserName,
                    ProblemsSolved = solvedProblems.Count,
                    TotalAttempts = submissions.Count,
                    AverageTime = avgTime,
                    SuccessRate = problemGroups.Any() ? (double)solvedProblems.Count / problemGroups.Count : 0
                };
            })
            .Where(s => s.TotalAttempts > 0)
            .OrderByDescending(s => s.ProblemsSolved)
            .ThenBy(s => s.AverageTime)
            .Take(20)
            .ToList();

            Console.WriteLine($"{"Rank",-6}{"Student",-20}{"Solved",-8}{"Success%",-10}{"Avg Time",-12}{"Attempts",-10}");
            Console.WriteLine(new string('-', 70));

            for (int i = 0; i < leaderboardData.Count; i++)
            {
                var entry = leaderboardData[i];
                var rank = i + 1;
                var rankDisplay = rank <= 3 ? $"🏆{rank}" : rank.ToString();

                Console.WriteLine($"{rankDisplay,-6}" +
                                $"{entry.StudentName,-20}" +
                                $"{entry.ProblemsSolved,-8}" +
                                $"{entry.SuccessRate * 100:F1}%{"",-6}" +
                                $"{entry.AverageTime:F1}m{"",-8}" +
                                $"{entry.TotalAttempts,-10}");
            }
        }
    }
}
