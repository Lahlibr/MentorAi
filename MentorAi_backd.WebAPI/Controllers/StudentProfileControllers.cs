using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MentorAi_backd.Application.DTOs.StudentDto;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/student/")]
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
        [HttpPut("Update")]
        public async Task<ActionResult<ApiResponse<StudentProfileDto>>> UpdateStudentProfileAsync(
    [FromBody] StudentUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponse<StudentProfileDto>.ErrorResponse(
                        "Validation failed",
                        errors,
                        400));
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(ApiResponse<StudentProfileDto>.ErrorResponse(
                        "User not authenticated",
                        null,
                        401));
                }

                var response = await _studentProfileService.UpdateStudentProfileAsync(userId, updateDto);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<StudentProfileDto>.ErrorResponse(
                    ex.Message,
                    null,
                    404));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ApiResponse<StudentProfileDto>.ErrorResponse(
                    ex.Message,
                    null,
                    400));
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, ApiResponse<StudentProfileDto>.ErrorResponse(
                    "An error occurred while updating the profile",
                    null,
                    500));
            }
        }

        [HttpGet("Dashboard")]
        public async Task<ActionResult<ApiResponse<StudentProfileDashboardDto>>> GetDashboard()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized(ApiResponse<StudentProfileDashboardDto>.ErrorResponse("User not authenticated", null, 401));

            var response = await _studentProfileService.GetDashboardAsync(userId);
            int statusCode = response?.StatusCode ?? 200;
            return StatusCode(statusCode, response);
        }



    }
}
