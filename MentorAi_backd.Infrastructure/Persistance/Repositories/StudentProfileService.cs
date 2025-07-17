using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly IGeneric<StudentProfile> _studentProfileRepo;
        private readonly IGeneric<User> _userRepo;
        private readonly IGeneric<ProblemAttempt> _problemAttemptRepo;
        private readonly IGeneric<StudentRoadmapProgress> _roadmapProgressRepo;
        private readonly IGeneric<StudentCertification> _studentCertificationRepo;
        private readonly IGeneric<UserBadge> _userBadgeRepo;
        private readonly IMapper _mapper;

        public StudentProfileService(
            IGeneric<StudentProfile> studentProfileRepo,
            IGeneric<User> userRepo,
            IGeneric<ProblemAttempt> problemAttemptRepo,
            IGeneric<StudentRoadmapProgress> roadmapProgressRepo,
            IGeneric<StudentCertification> studentCertificationRepo,
            IGeneric<UserBadge> userBadgeRepo,
            IMapper mapper)
        {
            _studentProfileRepo = studentProfileRepo;
            _userRepo = userRepo;
            _problemAttemptRepo = problemAttemptRepo;
            _roadmapProgressRepo = roadmapProgressRepo;
            _studentCertificationRepo = studentCertificationRepo;
            _userBadgeRepo = userBadgeRepo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<StudentProfileDto>> GetStudentProfileAsync(int userId)
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
            var studentProfileDto = _mapper.Map<StudentProfileDto>(profile);
            return new ApiResponse<StudentProfileDto>
            {
                Data = studentProfileDto,
                Message = "Student profile retrieved successfully.",
                Success = true
            };


        }
    }
}
