using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Queries;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.Infrastructure.Handlers
{
    public class GetLeaderboardHandler : IRequestHandler<GetLeaderBoardQuery, LeaderboardDto>
    {
        private readonly MentorAiDbContext _context;
        public GetLeaderboardHandler(MentorAiDbContext context)
        {
            _context = context;
        }
        public async Task<LeaderboardDto>Handle(GetLeaderBoardQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.StudentProfiles.ToListAsync(cancellationToken);
            var entries = new List<LeaderboardEntryDto>();

            foreach (var student in students)
            {
                var submissions = await _context.Submissions
                    .Where(s => s.StudentId == student.Id)
                    .Where(s => request.ProblemId == null || s.ProblemId == request.ProblemId)
                    .ToListAsync(cancellationToken);
                if(!submissions.Any()) continue;
                var problemGroups = submissions.GroupBy(s => s.ProblemId).ToList();
                var solvedProblems = problemGroups.Count(g => g.Any(s => s.IsCorrect)).ToList();

                var averageTime = 0.0;
                if (solvedProblems.Any())
                {
                    var times = solvedProblems.Select(g =>
                    {
                        var firstCorrect = g.FirstOrDefault(s => s.IsCorrect);
                        var firstAttempt = g.OrderBy(s => s.SubmissionTime).FirstOrDefault();
                        return (firstCorrect.SubmissionTime - firstAttempt.ProblemStartTime).TotalMinutes;
                    });
                    averageTime = times.Average();
                }
                entries.Add(new LeaderboardEntryDto
                {
                    StudentId = User.Id,
                    StudentName = User.UserName,
                    ProblemsSolved = solvedProblems.Count,
                    TotalAttempts = submissions.Count,
                    AverageTime = averageTime,
                    SuccessRate = submissions.Any() ? (double)solvedProblems.Count / submissions.Count : 0
                });
                var sortedEntries = entries.OrderByDescending(e => e.ProblemsSolved)
                    .ThenBy(e => e.AverageTime).ToList()
                    .Select((entry, index) =>
                    {
                        entry.Rank = index + 1;
                        return entry;
                    }).ToList();
                return new LeaderboardDto
                {
                    Entries = sortedEntries,
                    TotalStudents = sortedEntries.Count
                };

            };
        }
    }
}
