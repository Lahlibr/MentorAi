using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/code")]
    public class CodeExecutionController : ControllerBase
    {
        private readonly ICodeRunnerService _codeRunnerService;
        private readonly ILogger<CodeExecutionController> _logger;

        public CodeExecutionController(
            ICodeRunnerService codeRunnerService,
            ILogger<CodeExecutionController> logger)
        {
            _codeRunnerService = codeRunnerService;
            _logger = logger;
        }
        [HttpPost("execute")]
        public async Task<ActionResult<ApiResponse<CodeExecutionResultDto>>> ExecuteCode([FromBody] ExecuteCodeDto request)
        {
            var result = await _codeRunnerService.ExecuteSimpleCodeAsync(
                request.Code,
                request.Language,
                request.Input ?? "");
            return Ok(result);
        }


    }
}
