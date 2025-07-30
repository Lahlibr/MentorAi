using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using MentorAi_backd.Application.DTOs.ProblemDto;

namespace MentorAi_backd.Application.Queries
{
    public record GetStudentAnalyticsQuery(int StudentId) : IRequest<StudentAnalyticsDto>;
    public record GetLeaderBoardQuery(int? ProblemId = null) : IRequest<LeaderboardDto>;

    public record GetSubmissionStatusQuery(int SubmissionId) : IRequest<SubmissionStatusDto>;
}
