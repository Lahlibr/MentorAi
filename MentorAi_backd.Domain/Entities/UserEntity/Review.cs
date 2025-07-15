using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; } = default!; 

        public int ReviewerId { get; set; }
        public User Reviewer { get; set; } = default!;

        public int ProblemAttemptId { get; set; }
        public ProblemAttempt ProblemAttempt { get; set; } = default!;
        public DateTime ScheduledTime { get; set; }
        public DateTime? ActualReviewStartTime { get; set; }
        public DateTime? ActualReviewEndTime { get; set; }
        public ReviewStatusEnum Status { get; set; } 
        public string Feedback { get; set; } = string.Empty; 
        public double? RatingGivenByStudent { get; set; } 
        public string? VideoConferenceLink { get; set; } // Link for

    }
}