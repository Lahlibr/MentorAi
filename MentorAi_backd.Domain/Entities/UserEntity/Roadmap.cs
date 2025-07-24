using MentorAi_backd.Domain.Entities.Main;

namespace MentorAi_backd.Domain.Entities.UserEntity
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
        public int StudentId { get; set; }

        // Relationships
        public ICollection<Modules> Modules { get; set; } = new List<Modules>();
        

    }
}
