using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorAi_backd.WebAPI.Controllers;

[ApiController]
[Route("api/roadmaps")]
[Authorize(Roles = "Admin")]
public class RoadmapsController : ControllerBase
{
    private readonly IRoadmapService _roadmapService;

    public RoadmapsController(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    // GET: api/roadmaps
    [HttpGet]
    public async Task<IActionResult> GetAllRoadmaps()
    {
        var response = await _roadmapService.GetAllRoadmapsAsync();
        return Ok(response);
    }

    // GET: api/roadmaps/student/{studentId}
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentRoadmaps(int studentId)
    {
        var response = await _roadmapService.GetStudentRoadmapsAsync(studentId);
        return Ok(response);
    }

    // GET: api/roadmaps/by-student-id/{studentId}
    [HttpGet("by-student-id/{studentId}")]
    public async Task<IActionResult> GetActiveRoadmapByStudent(int studentId)
    {
        var response = await _roadmapService.GetRoadmapByStudentIdAsync(studentId);
        return Ok(response);
    }

    // POST: api/roadmaps
    [HttpPost]
    public async Task<IActionResult> CreateRoadmap([FromBody] CreateRoadmapDto dto)
    {
        var response = await _roadmapService.CreateRoadmapAsync(dto);
        return CreatedAtAction(nameof(GetAllRoadmaps), null, response);
    }

    // PUT: api/roadmaps/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoadmap(int id, [FromBody] UpdateRoadmapDto dto)
    {
        var response = await _roadmapService.UpdateRoadmapAsync(id, dto);
        return Ok(response);
    }

    // DELETE: api/roadmaps/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoadmap(int id)
    {
        var response = await _roadmapService.DeleteRoadmapAsync(id);
        return Ok(response);
    }

    // POST: api/roadmaps/enroll
    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollStudent([FromQuery] int studentId, [FromQuery] int roadmapId)
    {
        var response = await _roadmapService.EnrollStudentInRoadmapAsync(studentId, roadmapId);
        return Ok(response);
    }
}
