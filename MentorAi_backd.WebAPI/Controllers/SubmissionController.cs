using MediatR;
using MentorAi_backd.Application.Commands;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Queries;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    public class SubmissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubmissionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Submit([FromBody] CreateSubmissionDto submissionDto)
        {
            var submissionId = await _mediator.Send(new CreateSubmissionCommand(submissionDto));
            return Ok(new { SubmissionId = submissionId, Message = "Submission received and queued for grading" });
        }
        [HttpGet("{id}/status")]
        public async Task<ActionResult<SubmissionStatusDto>> GetStatus(int id)
        {
            var status = await _mediator.Send(new GetSubmissionStatusQuery(id));
            return Ok(status);
        }

    }
}
