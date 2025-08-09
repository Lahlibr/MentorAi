using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ModulesDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class ModulesService : IModulesService
    {
        private readonly IGenericRepository<LearningModule> _modules;
        private readonly IGenericRepository<Roadmap> _moduleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModulesService> _logger;
        private readonly IMapper _mapper;

        public ModulesService(IGenericRepository<LearningModule> modules,
            IGenericRepository<Roadmap> moduleRepo,
            IUnitOfWork unitOfWork,
            ILogger<ModulesService> logger,
            IMapper mapper)
        {
            _modules = modules;
            _moduleRepo = moduleRepo;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ModuleDto>>GetModuleByIdAsync(int moduleId)
        {
            
                var module = await _modules.GetByIdAsync(moduleId)
                             ?? throw new NotFoundException($"LearningModule with ID {moduleId} not found.");
                var dto = _mapper.Map<ModuleDto>(module);
                return ApiResponse<ModuleDto>.SuccessResponse(dto);
        }
        public async Task<ApiResponse<IEnumerable<ModuleDto>>> GetAllModuleAsync(int moduleId)
        {
            var modules = await _modules.GetAllAsync()
            ?? throw new NotFoundException("No modules found.");
            var dtoList = _mapper.Map<IEnumerable<ModuleDto>>(modules);
            return ApiResponse<IEnumerable<ModuleDto>>.SuccessResponse(dtoList);
        }

        public async Task<ApiResponse<ModuleDto>> UpdateModuleAsync(int moduleId , UpdateModuleDto updateDto)
        {
            if (updateDto == null)
            {
                return ApiResponse<ModuleDto>.ErrorResponse("Update data cannot be null.", 400);
            }

            var module = await _modules.GetByIdAsync(moduleId);
            if (module == null)
            {
                return ApiResponse<ModuleDto>.ErrorResponse($"LearningModule with ID {moduleId} not found.", 404);
            }
            _mapper.Map(updateDto, module);
             await _modules.UpdateAsync(module);
            await _unitOfWork.SaveChangesAsync();
            var updatedDto = _mapper.Map<ModuleDto>(module);
            return ApiResponse<ModuleDto>.SuccessResponse(updatedDto, "LearningModule updated successfully.");
        }

        public async Task<ApiResponse<ModuleDto>> AddModuleAsync(CreateModuleDto moduleDto)
        {
            if (moduleDto == null)
                throw new BadRequestException("LearningModule data cannot be null.");
            var module = _mapper.Map<LearningModule>(moduleDto);
            await _modules.AddAsync(module);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                               (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                
                return ApiResponse<ModuleDto>.ErrorResponse("A module with the same title already exists.", 400);
            }
            var createdDto = _mapper.Map<ModuleDto>(module);
            return ApiResponse<ModuleDto>.SuccessResponse(createdDto, "LearningModule added successfully.");
        }

        public async Task<ApiResponse<List<ModuleDto>>> AddBulkModulesAsync(List<CreateModuleDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                throw new BadRequestException("LearningModule list is empty.");
            var entities = _mapper.Map<List<LearningModule>>(dtos);
            await _modules.AddRangeAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<List<ModuleDto>>(entities);
            return ApiResponse<List<ModuleDto>>.SuccessResponse(result, "Modules added successfully.");
        }

        public async Task<ApiResponse<ModuleDto>> DeleteModuleAsync(int moduleId)
        {
            var module = await _modules.GetByIdAsync(moduleId);
            if (module == null)
                throw new NotFoundException($"LearningModule with ID {moduleId} not found.");
            _modules.Delete(module);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<ModuleDto>.SuccessResponse(null, "LearningModule deleted successfully.");
        }
    }
}
