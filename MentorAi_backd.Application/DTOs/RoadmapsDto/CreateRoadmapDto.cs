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

        public List<CreateModuleDto>? Modules { get; set; }
    }
}
