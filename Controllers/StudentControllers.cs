using MentorAi_backd.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentControllers : ControllerBase
    {
        private readonly MentorAiDbContext _context;
        public StudentControllers (MentorAiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStudents() =>Ok(_context.Students.ToList());
    }
}
