using MediatR;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnalyticsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("students/{studentId}")]
        public async Task<ActionResult<StudentAnalyticsDto>>  GetStudentAnalytics(int studentId)
        {
            var analytics = await _mediator.Send(new GetStudentAnalyticsQuery(studentId));
            if (analytics == null)
            {
                return NotFound();
            }
            return Ok(analytics);
        }
        [HttpGet("students/{studentId}/csv")]
        //Example use case: A teacher downloads student performance data for offline analysis or grading.
        public async Task<IActionResult> GetStudentAnalyticsCsv(int studentId)
        {
            var analytics = await _mediator.Send(new GetStudentAnalyticsQuery(studentId));
            if (analytics == null)
            {
                return NotFound();
            }
            var csv = GenerateCsv(analytics);
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv",$"student_{studentId}_analytics.csv");
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<LeaderboardDto>> GetLeaderboard([FromQuery] int? problemId = null)
        {
            var leaderboard = await _mediator.Send(new GetLeaderBoardQuery(problemId));
            return Ok(leaderboard);
        }

        private string GenerateCsv(StudentAnalyticsDto analytics)
        {
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Problem,Status,Attempts,Time Spent (min),Wrong Submissions,First Attempt,Solved Time");
            foreach (var perf in analytics.ProblemPerformances)
            {
                csv.AppendLine($"{perf.ProblemTitle},{perf.Status},{perf.Attempts},{perf.TimeSpent:F2},{perf.WrongSubmissions},{perf.FirstAttemptTime},{perf.SolvedTime}");
            }
            return csv.ToString();
        }

    }
}
