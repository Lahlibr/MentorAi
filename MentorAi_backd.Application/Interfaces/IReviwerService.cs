using MentorAi_backd.Application.DTOs.ReviwerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IReviwerService
    {
        Task<ApiResponse<ReviewerProfileDto>> GetReviwerProfileAsync(int userId);
        // Add other methods as needed, e.g., UpdateReviewerProfileAsync, etc.
        Task<ApiResponse<ReviewerUpdateDto>> UpdateReviwerProfile(int userId, ReviewerUpdateDto updateDto);

        Task<ApiResponse<IEnumerable<ReviewerProfileDto>>> GetAllReviewersAsync();
    }
}
