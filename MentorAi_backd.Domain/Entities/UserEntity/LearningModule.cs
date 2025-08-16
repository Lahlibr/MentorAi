using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Domain.Enums;


namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class LearningModule : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInRoadmap { get; set; }
        public RoadmapDifficultyLevelEnum difficulty { get; set; }

        // Foreign Key to Roadmap
        public string? ResourceType { get; set; }
        public string? ResourceIdentifier { get; set; }
        public string? Prerequisites { get; set; }
        public int EstimatedHours { get; set; } 
        public List<string> Topics { get; set; } = new();
        public List<string> LearningOutcomes { get; set; } = new();
        public int Projects { get; set; } 
        public bool IsLocked { get; set; } 
        public bool Certificate { get; set; } 

        public Roadmap? Roadmap { get; set; }
        public ICollection<RoadmapModule> RoadmapModules { get; set; }
        public ICollection<Problem> Problems { get; set; } = new List<Problem>();
        
    }
}
