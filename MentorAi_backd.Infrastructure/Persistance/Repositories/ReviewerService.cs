using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ReviwerDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Reviwer;
using MentorAi_backd.Domain.Entities.UserEntity;
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
        public async Task<ApiResponse<ReviewerProfileDto>> UpdateReviwerProfile(int userId , ReviewerProfileDto reviewer)
        {
            try
            {
                if(update)
                    
            }
        }
    }
}
