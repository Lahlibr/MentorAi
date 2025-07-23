using MentorAi_backd.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Nest;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("reviewers/pending")]
        public async Task<IActionResult> GetPendingReviewers()
        {
            var response = await _adminService.GetPendingReviewersAsync();
            return Ok(response);
        }
    
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _adminService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
