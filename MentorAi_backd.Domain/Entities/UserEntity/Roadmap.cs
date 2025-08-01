using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Domain.Entities.Student;
using System.Text.Json.Serialization;

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
        public ICollection<LearningModule> Modules { get; set; } = new List<LearningModule>();
        [JsonIgnore]
        public ICollection<StudentRoadmapProgress> Progresses { get; set; } = new List<StudentRoadmapProgress>();
        public ICollection<Problem> Problems { get; set; } = new List<Problem>();
        //public ICollection<Challenges> Challenges { get; set; } = new List<Challenges>();
        public ICollection<RoadmapModule> RoadmapModules { get; set; }

    }
}
