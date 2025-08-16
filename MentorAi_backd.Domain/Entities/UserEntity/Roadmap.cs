using MentorAi_backd.Domain.Entities.Main;
<<<<<<< Updated upstream
=======
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Domain.Entities.Student;
using System.Text.Json.Serialization;
using MentorAi_backd.Domain.Enums;
>>>>>>> Stashed changes

namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class Roadmap : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
<<<<<<< Updated upstream
=======
        public int price { get; set; }
        public int? originalPrice { get; set; }
>>>>>>> Stashed changes
        public string? ImageUrl { get; set; }
        public DifficultyLevelEnum DifficultyLevel { get; set; } 
        public int EstimatedCompletionHours { get; set; } 
        public int TotalModules { get; set; } = 0;
        public int TotalChallenges { get; set; } = 0;
        public int projects { get; set; } = 0;
        public bool isLocked { get; set; } = false;
        public bool isCompleted { get; set; } = false;
        public int TotalCertifications { get; set; } = 0;
       
        // Relationships
        public ICollection<Modules> Modules { get; set; } = new List<Modules>();

    }
}
