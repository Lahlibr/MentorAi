using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/student/profile")]
    [Authorize(Roles = "Student")]
    public class StudentProfileControllers : ControllerBase
    {
        private readonly IStudentProfileService _studentProfileService;

        public StudentProfileControllers(IStudentProfileService studentProfileService)
        {
            _studentProfileService = studentProfileService;
        }
        [HttpGet("Myprofile")]
        public async Task<ActionResult<ApiResponse<StudentProfileDto>>> GetStudentProfileAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("User not authenticated or invalid user ID.");
            }

            var response = await _studentProfileService.GetStudentProfileAsync(userId);
            return Ok(response);
        }




    }
}
