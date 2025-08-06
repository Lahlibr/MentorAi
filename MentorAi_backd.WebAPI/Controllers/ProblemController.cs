using AutoMapper;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Infrastructure.Persistance.Data;
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
        public ProblemController(MentorAiDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

    }
}
