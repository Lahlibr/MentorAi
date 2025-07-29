using MentorAi_backd.Application.DTOs.RoadmapDto;
using MentorAi_backd.Domain.Entities.UserEntity;


namespace MentorAi_backd.Application.Interfaces
{
    public interface IRoadmapService
    {
        Task<ApiResponse<RoadmapDto>> GetRoadmapByStudentIdAsync(int studentId);
        Task<ApiResponse<IEnumerable<RoadmapDto>>> GetAllRoadmapsAsync();
        Task<ApiResponse<IEnumerable<RoadmapDto>>> GetStudentAllRoadmapsAsync(int studentId);
        Task<ApiResponse<RoadmapDto>> CreateRoadmapAsync(CreateRoadmapDto dto);
        Task<ApiResponse<RoadmapDto>> AssignModulesAsync(int roadmapId, List<int> moduleIds);
        Task<ApiResponse<RoadmapDto>> UpdateRoadmapAsync(int roadmapId, UpdateRoadmapDto dto);
        Task<ApiResponse<RoadmapDto>> DeleteRoadmapAsync(int roadmapId);
        Task<ApiResponse<Roadmap>> EnrollStudentInRoadmapAsync(int studentId, int roadmapId);
    }
}
