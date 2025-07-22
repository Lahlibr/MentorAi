using MentorAi_backd.Application.DTOs.AdminDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<IEnumerable<ReviewerAdminViewDto>>> GetPendingReviewersAsync();
    }
}
