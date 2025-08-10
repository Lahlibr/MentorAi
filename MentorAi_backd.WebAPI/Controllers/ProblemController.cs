using AutoMapper;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/problems")]
    public class ProblemController : ControllerBase
    {
        private readonly MentorAiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProblemService _problem;
        public ProblemController(MentorAiDbContext context,IMapper mapper,IProblemService problem)
        {
            _context = context;
            _mapper = mapper;
            _problem = problem;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProblemDto>>> GetProblems()
        {
            var problems = await _context.Problems.ToListAsync();
            var problemDtos = _mapper.Map<List<ProblemDto>>(problems);
            return Ok(problemDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDto>> GetProblemById(int id)
        {
            var problemDto = await _context.Problems
                .Where(p => p.Id == id)
                .Select(p => new ProblemDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Difficulty = p.DifficultyLevel,
                    SampleTestCases = p.TestCases
                        .Where(tc => !tc.IsHidden)
                        .Select(tc => new TestCaseResultDto
                        {
                            Input = tc.Input,
                            ExpectedOutput = tc.ExpectedOutput
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (problemDto == null)
                return NotFound();

            return Ok(problemDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ProblemDto>>> CreateProblemAsync([FromBody] CreateProblemDto dto)
        {
            var res = await _problem.CreateAsync(dto);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _problem.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }




    }
}
