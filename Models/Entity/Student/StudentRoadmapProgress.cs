using MentorAi_backd.Models.Entity.UserEntity;

namespace MentorAi_backd.Models.Entity.Student
{
    public class StudentRoadmapProgress
    {
        public int Id { get; set; }

        // Refer to userid
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = default!;

        // Refer to Roadmap
        public int RoadmapId { get; set; }
        public Roadmap Roadmap { get; set; } = default!;

        public double CurrentProgressPercentage { get; set; } = 0.0;
        public int? CurrentModuleId { get; set; } 
        public Module? CurrentModule { get; set; }
        public DateTime? CompletionDate { get; set; } 
        public bool IsCompleted { get; set; } = false;
        public DateTime? LastActivityDate { get; set; }



    }
}
