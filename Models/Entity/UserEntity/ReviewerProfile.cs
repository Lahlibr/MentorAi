using MentorAi_backd.Models.Entity.Main;

namespace MentorAi_backd.Models.Entity.UserEntity
{
    public class ReviewerProfile : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!; 

        // Reviewer-specific fields
        public string Bio { get; set; } = string.Empty; 
        public int YearsOfExperience { get; set; } = 0;
        public string Availability { get; set; } = "Full-time"; 
        public string ExpertiseAreasJson { get; set; } = "[]"; 
        public double AverageRating { get; set; } = 0.0; 
        public int ReviewsCompleted { get; set; } = 0; 
        public bool IsAvailableForReviews { get; set; } = true;
    }
}