using MentorAi_backd.Application.DTOs.ModulesDto;
using MentorAi_backd.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers
{
    [ApiController]
    [Route("api/module")]
    [Authorize(Roles = "Admin")]
    public class ModulesController : ControllerBase
    {
        private readonly IModulesService _moduleService;

        public ModulesController(IModulesService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById(int moduleId)
        {
            var response = await _moduleService.GetModuleByIdAsync(moduleId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModules(int moduleId)
        {
            var response = await _moduleService.GetAllModuleAsync(moduleId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDto dto)
        {
            var response = await _moduleService.AddModuleAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBulkModules([FromBody] List<CreateModuleDto> dtos)
        {
            var response = await _moduleService.AddBulkModulesAsync(dtos);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{moduleId}")]
        public async Task<IActionResult> UpdateModule(int moduleId, [FromBody] UpdateModuleDto dto)
        {
            var response = await _moduleService.UpdateModuleAsync(moduleId, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            var response = await _moduleService.DeleteModuleAsync(moduleId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
