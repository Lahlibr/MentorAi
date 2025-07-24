using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
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
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IGeneric<User> userRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepo.Query()
                 .Include(u => u.ReviewerProfile)
                 .Include(u => u.StudentProfile)
                 .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"A user with ID {userId} does not exist.");
            }

            user.IsDeleted = true;
            user.Email = $"{user.Email}_deleted_{DateTime.UtcNow:yyyyMMddHHmmss}";
            user.RefreshToken = null;

            await _userRepo.UpdateAsync(user);

            if (user.ReviewerProfile  != null)
            {
                user.ReviewerProfile.IsDeleted = true;
                
            }
            if (user.StudentProfile != null)
            {
                user.StudentProfile.IsDeleted = true;
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}