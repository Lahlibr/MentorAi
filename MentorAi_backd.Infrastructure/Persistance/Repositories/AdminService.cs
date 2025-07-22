using AutoMapper;
using MentorAi_backd.Application.DTOs.AdminDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.WebAPI.Controllers
{
    public class AdminService : IAdminService
    {
        private readonly IGeneric<User> _userRepo;
        private readonly IMapper _mapper;
        
        public AdminService(IGeneric<User> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<ReviewerAdminViewDto>>> GetPendingReviewersAsync()
        {
            var pendingReviewers = await _userRepo.Query()
                .Include(u => u.ReviewerProfile)
                .Where(u => u.UserRole == UserEnum.Reviewer && u.ReviewerProfile.Status == ReviewerStatus.Pending)
                
                .ToListAsync();

            
            var dtos = _mapper.Map<IEnumerable<ReviewerAdminViewDto>>(pendingReviewers);
            return ApiResponse<IEnumerable<ReviewerAdminViewDto>>.SuccessResponse(dtos);
        }
    }
}
