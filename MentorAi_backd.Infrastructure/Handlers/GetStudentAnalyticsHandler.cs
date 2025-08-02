using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Queries;

namespace MentorAi_backd.Infrastructure.Handlers
{
    public class GetStudentAnalyticsHandler : IRequestHandler<GetStudentAnalyticsQuery, StudentAnalyticsDto>
    {
        private readonly MentorAi_backd _context;

        public GetStudentAnalyticsHandler(MentorAi_backd context)
        {
            _context = context;
        }
        public async Task<StudentAnalyticsDto> Handle(GetStudentAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);
            if (student == null)
                throw new ArgumentException("Student not found", nameof(request.StudentId));
            var submissions = await _context.Submissions
                .Include(s => s.Problem)
                .Where(s => s.StudentId == request.StudentId)
                .ToListAsync(cancellationToken);

            var problemGroups = submissions.GroupBy(s=>s.ProblemId)
                .Select(g => new ProblemPerformanceDto
                {
                    ProblemId = g.Key,
                    ProblemName = g.First().Problem.Name,
                    TotalAttempts = g.Count(),
                    SuccessfulAttempts = g.Count(s => s.IsCorrect),
                    AverageTimeToSolve = g.Average(s => s.ExecutionTime),
                    SuccessRate = (double)g.Count(s => s.IsCorrect) / g.Count()
                }).ToList();
            var problemPerformances = problemGroups.Select(pg =>
            {
                var firstCorrect = pg.Submissions.FirstOrDefault(s => s.IsCorrect);
                var firstAttempt = pg.Submissions.FirstOrDefault();

                return new ProblemPerformanceDto
                {
                    ProblemId = pg.ProblemId,
                    ProblemTitle = pg.ProblemName,
                    Attempts = pg.TotalAttempts,
                    Status = pg.SuccessfulAttempts > 0 ? "Solved" : "Attempted",
                    TimeSpent = firstCorrect != null ? (firstCorrect.SubmissionTime - firstAttempt.ProblemStartTime).TotalMinutes :
                    (pg.Submissions.Last().SubmissionTime - firstAttempt.ProblemStartTime).TotalMinutes,
                    WrongSubmissions = firstAttempt != null
                        ? pg.Submissions.Count(s => !s.IsCorrect && s.SubmissionTime<firstCorrect.SubmissionTime) : pg.Submissions.Count,
                    FirstAttemptTime = firstAttempt?.SubmissionTime,
                    SolvedTime = firstCorrect?.SubmissionTime
                };
                }).ToList();
            var solvedProblems = problemPerformances.Where(p=> p.Status == "Solved").ToList();
            return new StudentAnalyticsDto
            {
                StudentId = student.Id,
                StudentName = student.Name,
                ProblemsSolved = solvedProblems.Count,
                TotalAttempts = submissions.Count,
                AverageTimeToSolve = solvedProblems.Any() ? solvedProblems.Average(p => p.TimeSpent) : 0,
                SuccessRate = submissions.Any() ? (double)solvedProblems.Count / submissions.Count : 0,
                ProblemPerformances = problemPerformances
            };
        }

        public class GetLeaderboardHandler : IRequestHandler<GetLeaderBoardQuery, LeaderboardDto>
        {
            private readonly MentorAi_backd _context;
            public GetLeaderboardHandler(MentorAi_backd context)
            {
                _context = context;
            }
            public async Task<LeaderboardDto> Handle(GetLeaderBoardQuery request, CancellationToken cancellationToken)
            {
               var student = await _context.Students.ToListAsync(cancellationToken);
               var entries = new List<LeaderboardEntryDto>();
            }
        }
    }

}
