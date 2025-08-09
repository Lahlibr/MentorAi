using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Application.DTOs.StudentDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Reviwer;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly IGenericRepository<StudentProfile> _studentProfileRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<ProblemAttempt> _problemAttemptRepo;
        private readonly IGenericRepository<StudentRoadmapProgress> _roadmapProgressRepo;
        private readonly IGenericRepository<StudentCertification> _studentCertificationRepo;
        private readonly IGenericRepository<UserBadge> _userBadgeRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentProfileService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Review> _reviewRepo;

        public StudentProfileService(
            IGenericRepository<StudentProfile> studentProfileRepo,
            IGenericRepository<User> userRepo,
            IGenericRepository<ProblemAttempt> problemAttemptRepo,
            IGenericRepository<StudentRoadmapProgress> roadmapProgressRepo,
            IGenericRepository<StudentCertification> studentCertificationRepo,
            IGenericRepository<UserBadge> userBadgeRepo,
            IMapper mapper,
            ILogger<StudentProfileService> logger,
            IUnitOfWork unitOfWork,
            IGenericRepository<Review> reviewRepo)
        {
            _studentProfileRepo = studentProfileRepo;
            _userRepo = userRepo;
            _problemAttemptRepo = problemAttemptRepo;
            _roadmapProgressRepo = roadmapProgressRepo;
            _studentCertificationRepo = studentCertificationRepo;
            _userBadgeRepo = userBadgeRepo;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _reviewRepo = reviewRepo;
        }

        public async Task<ApiResponse<StudentProfileDto>> GetStudentProfileAsync(int userId)
        {
            try
            {
                var profile = await _userRepo.Query()
                    .Include(u => u.StudentProfile)
                        .ThenInclude(sp => sp.RoadmapProgresses)
                            .ThenInclude(rp => rp.Roadmap)
                    .Include(u => u.StudentProfile)
                        .ThenInclude(sp => sp.RoadmapProgresses)
                        .ThenInclude(srp => srp.CurrentModule)
                    .Include(u => u.StudentProfile)
                        .ThenInclude(sp => sp.ProblemAttempts)
                        .ThenInclude(pa => pa.Problem)
                    .Include(u => u.StudentProfile)
                        .ThenInclude(sp => sp.StudentCertifications)
                    .Include(u => u.StudentProfile)
                        .ThenInclude(sp => sp.UserBadges)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (profile == null || profile.StudentProfile == null)
                {
                    throw new NotFoundException($"Student profile for user ID {userId} not found or user is not a student.");
                }

                var studentProfileDto = _mapper.Map<StudentProfileDto>(profile.StudentProfile);

                _logger.LogInformation("Successfully retrieved profile for user {UserId}", userId);

                return new ApiResponse<StudentProfileDto>
                {
                    Data = studentProfileDto,
                    Message = "Student profile retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student profile for user {UserId}", userId);
                throw;
            }
        }

        public async Task<ApiResponse<StudentProfileDto>> UpdateStudentProfileAsync(int userId, StudentUpdateDto updateDto)
        {
            try
            {
                _logger.LogInformation("Starting profile update for user {UserId}", userId);

                // Validate input
                if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

                // Load user with profile
                var user = await _userRepo.Query()
                    .Include(u => u.StudentProfile)
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    ?? throw new NotFoundException($"User with ID {userId} not found.");

                if (user.StudentProfile == null)
                    throw new NotFoundException($"Student profile for user ID {userId} not found.");

                if (user.UserRole != UserEnum.Student)
                    throw new BadRequestException($"User with ID {userId} is not a student.");

                // Update user properties
                if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
                {
                    if (await _userRepo.Query().AnyAsync(u =>
                        u.PhoneNumber == updateDto.PhoneNumber && u.Id != userId))
                    {
                        throw new BadRequestException("Phone number already exists.");
                    }
                    user.PhoneNumber = updateDto.PhoneNumber;
                }

                if (!string.IsNullOrEmpty(updateDto.ProfileImageUrl))
                {
                    user.ProfileImageUrl = updateDto.ProfileImageUrl;
                }

               
                _mapper.Map(updateDto, user.StudentProfile);

             
                _userRepo.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                var savedProfile = await _studentProfileRepo.GetByIdAsync(userId);
                _logger.LogInformation("Saved profile: {@Profile}", savedProfile);

                // Return complete updated profile
                var response = await GetStudentProfileAsync(userId);
                _logger.LogInformation("Response data: {@Response}", response.Data);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", userId);
                throw;
            }
        }

        public async Task<ApiResponse<StudentProfileDashboardDto>> GetDashboardAsync(int userId)
        {
            var studentProfile = await _studentProfileRepo.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (studentProfile == null)
                return ApiResponse<StudentProfileDashboardDto>.ErrorResponse("Student profile not found", null, 404);

            var attempts = await _problemAttemptRepo.Query()
                .Include(pa => pa.Problem)
                .Where(pa => pa.StudentProfile.UserId == userId)
                .OrderByDescending(pa => pa.AttemptTime)
                .ToListAsync();

            var problemsSolved = attempts
                .GroupBy(a => a.ProblemId)
                .Count(g => g.Any(a => a.IsCorrect));

            var recentProblems = attempts
                .GroupBy(a => a.ProblemId)
                .Select(g =>
                {
                    var last = g.OrderByDescending(x => x.AttemptTime).First();
                    return new RecentProblemDto
                    {
                        ProblemId = g.Key,
                        Title = last.Problem.Title,
                        IsSolved = g.Any(x => x.IsCorrect),
                        LastAttempted = last.AttemptTime
                    };
                })
                .Take(5)
                .ToList();
            var reviewsReceived = await _reviewRepo.Query()
                .CountAsync(r => r.StudentId == userId);

            
            double averageScore = problemsSolved > 0 ? 8.5 : 0; 
            double skillGrowth = 0.15; 

            var dto = new StudentProfileDashboardDto
            {
                ProblemsSolved = problemsSolved,
                ReviewsReceived = reviewsReceived,
                AverageScore = averageScore,
                SkillGrowth = skillGrowth,
                RecentProblems = recentProblems
            };

            return ApiResponse<StudentProfileDashboardDto>.SuccessResponse(dto, "Dashboard loaded successfully");
        }
    }



    
}