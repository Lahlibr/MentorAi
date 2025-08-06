using MentorAi_backd.Domain.Entities.UserEntity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MentorAi_backd.Domain.Entities.Student
{
    public class StudentRoadmapProgress
    {
        

        public int Id { get; set; }

        // Refer to userid
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = default!;

        // Refer to Roadmap
        [ForeignKey("Roadmap")]
        public int RoadmapId { get; set; }
        [JsonIgnore]
        public Roadmap Roadmap { get; set; } = default!;

        public double thresholdmark { get; set; } = 0.0;
        public double CurrentProgressPercentage { get; set; } = 0.0;
        public int? CurrentModuleId { get; set; } 
        public LearningModule? CurrentModule { get; set; }
        public DateTime? CompletionDate { get; set; } 
        public bool IsCompleted { get; set; } = false;
        public DateTime? LastActivityDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
