using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Student;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeneric<Roadmap> _roadmapRepo;
        private readonly IGeneric<StudentRoadmapProgress> _progressRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<RoadmapService> _logger;
        private readonly IGeneric<LearningModule> _moduleRepo;

        public RoadmapService(
            IUnitOfWork unitOfWork,
            IGeneric<Roadmap> roadmapRepo,
            IGeneric<StudentRoadmapProgress> progressRepo,
            IMapper mapper,
            ILogger<RoadmapService> logger,
            IGeneric<LearningModule> moduleRepo)
        {
            _unitOfWork = unitOfWork;
            _roadmapRepo = roadmapRepo;
            _progressRepo = progressRepo;
            _mapper = mapper;
            _logger = logger;
            _moduleRepo = moduleRepo;
        }

        public async Task<ApiResponse<RoadmapDto>> GetRoadmapByStudentIdAsync(int studentId)
        {
            var activeProgress = await _progressRepo.Query()
                .Where(p => p.StudentProfile.UserId == studentId && p.IsActive && !p.IsDeleted)
                .Include(p => p.Roadmap)
                .ThenInclude(r => r.RoadmapModules)
                .ThenInclude(rm => rm.LearningModule)
                .FirstOrDefaultAsync();
            if (activeProgress == null)
            {
                throw new NotFoundException("No active roadmap found for the student.");
            }
            var roadmapDto = _mapper.Map<RoadmapDto>(activeProgress.Roadmap);
            roadmapDto.ProgressPercentage = activeProgress.CurrentProgressPercentage;
            return ApiResponse<RoadmapDto>.SuccessResponse(roadmapDto,"Roadmap retrieved successfully");
        }

        public async Task<ApiResponse<IEnumerable<RoadmapDto>>>GetAllRoadmapsAsync()
        {
            var roadmaps = await _roadmapRepo.Query()
                .Where(r => !r.IsDeleted)
                .Include(r => r.RoadmapModules).ThenInclude(rm => rm.LearningModule)
                .Include(r => r.Progresses)
                .ToListAsync();



            var roadmapDtos = _mapper.Map<IEnumerable<RoadmapDto>>(roadmaps);
            return ApiResponse<IEnumerable<RoadmapDto>>.SuccessResponse(roadmapDtos, "Roadmaps retrieved successfully");
        }
        public async Task<ApiResponse<IEnumerable<RoadmapDto>>> GetStudentAllRoadmapsAsync(int studentId)
        {
            var enrollments = await _progressRepo.Query()
                .Where(p => p.StudentProfileId == studentId && p.IsActive && !p.IsDeleted)
                .Include(p => p.Roadmap)
                .ThenInclude(r => r.RoadmapModules)
                .ThenInclude(rm => rm.LearningModule)
                .ToListAsync();

            var roadmapDtos = _mapper.Map<IEnumerable<RoadmapDto>>(enrollments.Select(e => e.Roadmap));
            return ApiResponse<IEnumerable<RoadmapDto>>.SuccessResponse(roadmapDtos, "Student enrolled roadmaps.");
        }



        public async Task<ApiResponse<RoadmapDto>> CreateRoadmapAsync( CreateRoadmapDto dto)
        {
            var roadmap = _mapper.Map<Roadmap>(dto);
            roadmap.IsDeleted = false;
            roadmap.IsActive = true;
            if (dto.Modules != null && dto.Modules.Any())
            {
                roadmap.Modules = new List<LearningModule>();
                foreach (var moduleDto in dto.Modules)
                {
                    var module = _mapper.Map<LearningModule>(moduleDto);
                    module.IsDeleted = false;
                    
                    roadmap.Modules.Add(module);
                }
            }
            await _roadmapRepo.AddAsync(roadmap);
            await _unitOfWork.SaveChangesAsync();
            var roadmapDto = _mapper.Map<RoadmapDto>(roadmap);
            return ApiResponse<RoadmapDto>.SuccessResponse(_mapper.Map<RoadmapDto>(roadmap), "Roadmap created successfully");

        }

        public async Task<ApiResponse<RoadmapDto>> AssignModulesAsync(int roadmapId, List<int> moduleIds)
        {
            var roadmap = await _roadmapRepo.Query()
                    .Include(r => r.RoadmapModules)
                       .ThenInclude(rm => rm.LearningModule)
                    .FirstOrDefaultAsync(r => r.Id == roadmapId && !r.IsDeleted);
            if (roadmap == null)
                throw new NotFoundException($"Roadmap with ID {roadmapId} not found.");
            var modules = await _moduleRepo.Query()
                .Where(m=>moduleIds.Contains(m.Id)&& !m.IsDeleted)
                .ToListAsync();
            if (roadmap.RoadmapModules == null)
            {
                roadmap.RoadmapModules = new List<RoadmapModule>();
            }
            var existingModuleIds = roadmap.RoadmapModules.Select(rm => rm.ModuleId).ToHashSet();

            for (int i= 0; i < moduleIds.Count; i++)
            {
                int moduleId = moduleIds[i];
                {
                    if (!existingModuleIds.Contains(moduleId))
                    {
                        roadmap.RoadmapModules.Add(new RoadmapModule
                        {
                            RoadmapId = roadmapId,
                            ModuleId = moduleId,
                            Order = 0
                        });
                    }
                }
            }
            
            await _unitOfWork.SaveChangesAsync();
            var roadmapDto = _mapper.Map<RoadmapDto>(roadmap);
            return ApiResponse<RoadmapDto>.SuccessResponse(roadmapDto, "Modules assigned to roadmap successfully");
        }
        public async Task<ApiResponse<RoadmapDto>> UpdateRoadmapAsync(int roadmapId, UpdateRoadmapDto dto)
        {
            var existingRoadmap = await _roadmapRepo.GetByIdAsync(roadmapId)
            
                ?? throw new NotFoundException("Roadmap not found.");
            
            _mapper.Map(dto, existingRoadmap);
            await _roadmapRepo.UpdateAsync(existingRoadmap);
            await _unitOfWork.SaveChangesAsync();
            var mapperrst = _mapper.Map<RoadmapDto>(existingRoadmap);
            return ApiResponse<RoadmapDto>.SuccessResponse(mapperrst, "Roadmap updated successfully");
        }

        public async Task<ApiResponse<RoadmapDto>> DeleteRoadmapAsync(int roadmapId)
        {
           
            var roadmap = await _roadmapRepo.Query()
                .Include(r => r.Modules)
                .FirstOrDefaultAsync(r => r.Id == roadmapId);

           
            if (roadmap == null)
                throw new NotFoundException($"Roadmap with ID {roadmapId} not found.");

            roadmap.IsDeleted = true;
            roadmap.IsActive = false;
            _roadmapRepo.UpdateAsync(roadmap);


            foreach (var roadmapModule in roadmap.Modules)
            {
                roadmapModule.IsDeleted = true;
                _moduleRepo.UpdateAsync(roadmapModule); // synchronous
            }



            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Soft-deleted roadmap {RoadmapId}", roadmapId);

           
            var roadmapDto = _mapper.Map<RoadmapDto>(roadmap);
            return ApiResponse<RoadmapDto>.SuccessResponse(roadmapDto, "Roadmap deleted successfully.");
        }


        public async Task<ApiResponse<Roadmap>> EnrollStudentInRoadmapAsync(int studentId, int roadmapId)
        {
            var roadmap = await _roadmapRepo.Query()
                .FirstOrDefaultAsync(r => r.Id == roadmapId);

            if (roadmap == null || roadmap.IsDeleted)
                throw new BadRequestException("This roadmap is no longer available for enrollment.");

            var alreadyEnrolled = await _progressRepo.Query()
                .AnyAsync(p => p.StudentProfileId == studentId && p.RoadmapId == roadmapId && !p.IsDeleted);

            if (alreadyEnrolled)
                throw new BadRequestException("You are already enrolled in this roadmap.");

            var progress = new StudentRoadmapProgress
            {
                StudentProfileId = studentId, // ✅ use foreign key
                RoadmapId = roadmapId,
                IsActive = true,
                CurrentProgressPercentage = 0
            };

            await _progressRepo.AddAsync(progress);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<Roadmap>.SuccessResponse(roadmap, "Successfully enrolled in the roadmap.");
        }




    }
}
