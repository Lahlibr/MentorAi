


using MentorAi_backd.Application.DTOs.ModulesDto;

namespace MentorAi_backd.Application.DTOs.RoadmapDto
{
    public class RoadmapDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime GeneratedOn { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public List<ModuleDto> Modules { get; set; } = new();
        public int EnrolledCount { get; set; }
        public double ProgressPercentage { get; set; }
    }
}
