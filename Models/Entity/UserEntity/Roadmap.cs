using MentorAi_backd.Models.Entity.Main;

namespace MentorAi_backd.Models.Entity.UserEntity
{
    public class Roadmap : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string DifficultyLevel { get; set; } = string.Empty;
        public int EstimatedCompletionHours { get; set; } 
        public int TotalModules { get; set; } = 0;
        public int TotalChallenges { get; set; } = 0;
        public int TotalCertifications { get; set; } = 0;
       
        // Relationships
        public ICollection<Module> Modules { get; set; } = new List<Module>();

    }
}
