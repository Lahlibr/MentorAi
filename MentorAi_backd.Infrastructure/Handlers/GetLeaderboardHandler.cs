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

        public async Task<LeaderboardDto> Handle(GetLeaderBoardQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.StudentProfiles.ToListAsync(cancellationToken);
            var entries = new List<LeaderboardEntryDto>();

            foreach (var student in students)
            {
                var submissions = await _context.Submissions
                    .Where(s => s.StudentId == student.UserId)
                    .Where(s => request.ProblemId == null || s.ProblemId == request.ProblemId)
                    .ToListAsync(cancellationToken);

                if (!submissions.Any()) continue;

                var problemGroups = submissions.GroupBy(s => s.ProblemId).ToList();

                var solvedProblemGroups = problemGroups.Where(g => g.Any(s => s.IsCorrect)).ToList();

                double averageTime = 0.0;
                if (solvedProblemGroups.Any())
                {
                    var times = solvedProblemGroups.Select(g =>
                    {
                        var firstCorrect = g.FirstOrDefault(s => s.IsCorrect);
                        var firstAttempt = g.OrderBy(s => s.SubmissionTime).FirstOrDefault();
                        if (firstCorrect != null && firstAttempt != null)
                            return (firstCorrect.SubmissionTime - firstAttempt.ProblemStartTime).TotalMinutes;
                        return 0.0;
                    }).Where(t => t > 0); // exclude zeros just in case

                    if (times.Any())
                        averageTime = times.Average();
                }

                entries.Add(new LeaderboardEntryDto
                {
                    StudentId = student.UserId,
                    StudentName = student.User.UserName,
                    ProblemsSolved = solvedProblemGroups.Count,
                    TotalAttempts = submissions.Count,
                    AverageTime = averageTime,
                    SuccessRate = problemGroups.Any() ? (double)solvedProblemGroups.Count / problemGroups.Count() : 0,
                });
            }

            var sortedEntries = entries
                .OrderByDescending(e => e.ProblemsSolved)
                .ThenBy(e => e.AverageTime)
                .Select((entry, index) =>
                {
                    entry.Rank = index + 1;
                    return entry;
                })
                .ToList();

            return new LeaderboardDto
            {
                Entries = sortedEntries,
                TotalStudents = sortedEntries.Count
            };
        }
    }

}
