using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeneric<Roadmap> _roadmapRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<RoadmapService> _logger;
        private readonly IGeneric<Module> _moduleRepo;

        public RoadmapService(
            IUnitOfWork unitOfWork,
            IGeneric<Roadmap> roadmapRepo,
            IMapper mapper,
            ILogger<RoadmapService> logger,
            IGeneric<Module> moduleRepo)
        {
            _unitOfWork = unitOfWork;
            _roadmapRepo = roadmapRepo;
            _mapper = mapper;
            _logger = logger;
            _moduleRepo = moduleRepo;
        }

        public async Task<ApiResponse<RoadmapDto>> GetRoadmapByStudentIdAsync(int studentId)
        {
            var roadmap = await _roadmapRepo.Query()
                    .Include(r => r.Modules)
                       .ThenInclude(m=>m.Problems)
                    .FirstOrDefaultAsync(r=> r.StudentId == studentId && r.IsActive && !r.IsDeleted)
                ?? throw new NotFoundException($"Roadmap with ID {studentId} not found.");
            var dto = _mapper.Map<RoadmapDto>(roadmap);
            return ApiResponse<RoadmapDto>.SuccessResponse(dto);
        }

        
    }
}
