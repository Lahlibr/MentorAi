using MentorAi_backd.Application.DTOs.ModulesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IModuleService
    {
        Task<ApiResponse<ModuleDto>> GetModuleByIdAsync(int moduleId);
        Task<ApiResponse<IEnumerable<ModuleDto>>> GetAllModuleAsync(int moduleId);
        Task<ApiResponse<ModuleDto>> UpdateModuleAsync(int moduleId, UpdateModuleDto updateDto);
        Task<ApiResponse<ModuleDto>> AddModuleAsync(CreateModuleDto moduleDto);
        Task<ApiResponse<List<ModuleDto>>> AddBulkModulesAsync(List<CreateModuleDto> dtos);
        Task<ApiResponse<ModuleDto>> DeleteModuleAsync(int moduleId);

    }
}
