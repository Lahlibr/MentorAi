using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ReviwerDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Reviwer;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class ReviewerService : IReviwerService
    {
        private readonly  IGeneric<ReviewerProfile> _reviewer;
        private readonly IGeneric<User> _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewerService> _logger;

        public ReviewerService(IGeneric<ReviewerProfile> reviewer,
            IGeneric<User> userRepo,
            IMapper mapper,
            ILogger<ReviewerService> logger)
        {
            _reviewer = reviewer;
            _userRepo = userRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResponse<ReviewerProfileDto>> GetReviwerProfileAsync(int userId)
        {
            try
            {
                var reviewer = await _userRepo.Query()
                        .Include(u => u.ReviewerProfile)
                        .FirstOrDefaultAsync(u => u.Id == userId);
                if (reviewer == null || reviewer.ReviewerProfile == null)
                    throw new NotFoundException($"Reviewer profile for user ID {userId} not found or user is not a reviewer.");
                var profileDto = _mapper.Map<ReviewerProfileDto>(reviewer.ReviewerProfile);
                
                return ApiResponse<ReviewerProfileDto>.SuccessResponse(profileDto, "Reviewer profile retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reviewer profile for user ID {UserId}", userId);
                throw;
            }

        }
        public async Task<ApiResponse<ReviewerUpdateDto>> UpdateReviwerProfile(int userId, ReviewerUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null) throw new ArgumentNullException(nameof(updateDto), "Reviewer profile data cannot be null.");

                var user = await _userRepo.Query()
                    .Include(u => u.ReviewerProfile)
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    ?? throw new NotFoundException($"User with ID {userId} not found.");
                if (user.UserRole != UserEnum.Reviewer)
                    throw new BadRequestException($"User with ID {userId} is not a reviewer.");
                if (user.ReviewerProfile == null)
                    throw new NotFoundException($"Reviewer profile for user ID {userId} not found.");
                _mapper.Map(updateDto, user.ReviewerProfile);
                await _userRepo.UpdateAsync(user);

                var savedProfile = await _reviewer.GetByIdAsync(userId);
                _logger.LogInformation("Saved profile: {@Profile}", savedProfile);

                var response = await GetReviwerProfileAsync(userId);
                _logger.LogInformation("Response data: {@Response}", response.Data);
                return ApiResponse<ReviewerUpdateDto>.SuccessResponse(_mapper.Map<ReviewerUpdateDto>(savedProfile), "Reviewer profile updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating reviewer profile for user ID {UserId}", userId);
                return ApiResponse<ReviewerUpdateDto>.ErrorResponse("An error occurred while updating the reviewer profile.", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<ReviewerProfileDto>>> GetAllReviewersAsync()
        {
            try
            {
              var getAll = await _userRepo.Query() // null reference default
                    .Include(u => u.ReviewerProfile)
                        .ThenInclude(rp => rp.User)
                    .Where(u => u.UserRole == UserEnum.Reviewer)
                    .Select(u => u.ReviewerProfile)
                    .ToListAsync();
                if (getAll == null || !getAll.Any())
                    throw new NotFoundException("No reviewers found.");
                var reviewerDtos = _mapper.Map<IEnumerable<ReviewerProfileDto>>(getAll);
                return ApiResponse<IEnumerable<ReviewerProfileDto>>.SuccessResponse(reviewerDtos, "All reviewers retrieved successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all reviewers.");
                return ApiResponse<IEnumerable<ReviewerProfileDto>>.ErrorResponse("An error occurred while retrieving reviewers.", ex.Message);
            }
        }
    }
}
