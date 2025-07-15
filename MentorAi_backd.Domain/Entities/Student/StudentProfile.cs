using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.UserEntity;


namespace MentorAi_backd.Domain.Entities.Student
{
    public class StudentProfile : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public int? Age { get; set; } 
        public int? AssessmentScore { get; set; }

        public string? CurrentLearningGoal { get; set; }
        public string? PreferredLearningStyle { get; set; }
        public ICollection<StudentRoadmapProgress> RoadmapProgresses { get; set; } = new List<StudentRoadmapProgress>();
        public ICollection<ProblemAttempt> ProblemAttempts { get; set; } = new List<ProblemAttempt>();
        public ICollection<StudentCertification> StudentCertifications { get; set; } = new List<StudentCertification>();
        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

    }
}
