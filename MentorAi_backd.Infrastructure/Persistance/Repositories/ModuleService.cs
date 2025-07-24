using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ModulesDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.Extensions.Logging;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class ModuleService : IModuleService
    {
        private readonly IGeneric<Module> _modules;
        private readonly IGeneric<Roadmap> _moduleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModuleService> _logger;
        private readonly IMapper _mapper;

        public ModuleService(IGeneric<Module> modules,
            IGeneric<Roadmap> moduleRepo,
            IUnitOfWork unitOfWork,
            ILogger<ModuleService> logger,
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
                             ?? throw new NotFoundException($"Module with ID {moduleId} not found.");
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
                return ApiResponse<ModuleDto>.ErrorResponse($"Module with ID {moduleId} not found.", 404);
            }
            _mapper.Map(updateDto, module);
             await _modules.UpdateAsync(module);
            await _unitOfWork.SaveChangesAsync();
            var updatedDto = _mapper.Map<ModuleDto>(module);
            return ApiResponse<ModuleDto>.SuccessResponse(updatedDto, "Module updated successfully.");
        }

        public async Task<ApiResponse<ModuleDto>> AddModuleAsync(CreateModuleDto moduleDto)
        {
            if (moduleDto == null)
                throw new BadRequestException("Module data cannot be null.");
            var module = _mapper.Map<Module>(moduleDto);
            await _modules.AddAsync(module);
            await _unitOfWork.SaveChangesAsync();
            var createdDto = _mapper.Map<ModuleDto>(module);
            return ApiResponse<ModuleDto>.SuccessResponse(createdDto, "Module added successfully.");
        }

        public async Task<ApiResponse<List<ModuleDto>>> AddBulkModulesAsync(List<CreateModuleDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                throw new BadRequestException("Module list is empty.");
            var entities = _mapper.Map<List<Module>>(dtos);
            await _modules.AddRangeAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<List<ModuleDto>>(entities);
            return ApiResponse<List<ModuleDto>>.SuccessResponse(result, "Modules added successfully.");
        }

        public async Task<ApiResponse<ModuleDto>> DeleteModuleAsync(int moduleId)
        {
            var module = await _modules.GetByIdAsync(moduleId);
            if (module == null)
                throw new NotFoundException($"Module with ID {moduleId} not found.");
            _modules.Delete(module);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<ModuleDto>.SuccessResponse(null, "Module deleted successfully.");
        }
    }
}
