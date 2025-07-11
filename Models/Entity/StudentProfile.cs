namespace MentorAi_backd.Models.Entity
{
    public class StudentProfile : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public int? Age { get; set; } 
        public int? AssessmentScore { get; set; }

        public string? CurrentLearningGoal { get; set; }
        public string? PreferredLearningStyle { get; set; }

    }
}
