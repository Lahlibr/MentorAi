using MentorAi_backd.Application.DTOs.ModulesDto;

using System.ComponentModel.DataAnnotations;


namespace MentorAi_backd.Application.DTOs.RoadmapDto
{
    public class CreateRoadmapDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1_000)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public string? Category { get; set; }
        public string DifficultyLevel { get; set; } = string.Empty;
        public int EstimatedCompletionHours { get; set; }
        public int TotalModules { get; set; } = 0;
        public int TotalChallenges { get; set; } = 0;
        public int TotalCertifications { get; set; } = 0;
   
        public List<CreateModuleDto>? Modules { get; set; }
    }
}
