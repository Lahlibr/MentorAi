using MentorAi_backd.Application.DTOs.ReviwerDto;
using MentorAi_backd.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/reviewer-profile")]
    [Authorize]
    public class ReviewerProfileControllers : ControllerBase
    {
        private readonly IReviwerService _reviewerService;

        public ReviewerProfileControllers(IReviwerService reviewerService)
        {
            _reviewerService = reviewerService;
        }
        [HttpGet("ReviwerProfile")]
        [Authorize(Roles = "Reviewer,Admin")]
        public async Task<ActionResult<ApiResponse<ReviewerProfileDto>>> GetReviwerProfile(int? id = null)
        {
            int reviewerIdToFetch;
            if (id.HasValue)
            {
                // If an ID is provided, only allow Admin to fetch arbitrary profiles
                if (!User.IsInRole("Admin"))
                {
                    return Forbid(ApiResponse<ReviewerProfileDto>.ErrorResponse("Access denied. Only Admins can view other reviewer profiles.", 403).Message);
                }
                reviewerIdToFetch = id.Value;
            }
            else
            {
                // If no ID is provided, fetch the authenticated user's profile
                var reviewerIdClaim = User.FindFirstValue("ReviewerId"); // Assuming you store ReviewerId in claims
                if (string.IsNullOrEmpty(reviewerIdClaim) || !int.TryParse(reviewerIdClaim, out reviewerIdToFetch))
                {
                    return Unauthorized(ApiResponse<ReviewerProfileDto>.ErrorResponse("Reviewer ID claim not found or invalid.", 401).Message);
                }
            }

            var response = await _reviewerService.GetReviwerProfileAsync(reviewerIdToFetch);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateReviwerProfile")]
        [Authorize(Roles = "Reviewer")]
        public async Task<ActionResult<ApiResponse<ReviewerUpdateDto>>> UpdateReviwerProfile([FromBody] ReviewerUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest(ApiResponse<ReviewerUpdateDto>.ErrorResponse("Reviewer profile data cannot be null.", 400).Message);
            }
            var reviewerIdClaim = User.FindFirstValue("ReviewerId");
            if (string.IsNullOrEmpty(reviewerIdClaim) || !int.TryParse(reviewerIdClaim, out int reviewerId))
            {
                return Unauthorized(ApiResponse<ReviewerUpdateDto>.ErrorResponse("Reviewer ID claim not found or invalid.", 401).Message);
            }
            var response = await _reviewerService.UpdateReviwerProfile(reviewerId, updateDto);
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllReviewerProfile")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ReviewerProfileDto>>> GetReviewerProfile()
        {
            
            var response = await _reviewerService.GetAllReviewersAsync();
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
