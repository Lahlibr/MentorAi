using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Reviewer;
using MentorAi_backd.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class ReviewerProfile : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; } = default!; 

        // Reviewer-specific fields
        public string Bio { get; set; } = string.Empty; 
        public int YearsOfExperience { get; set; } = 0;
        public ReviewerStatus Status { get; set; } = ReviewerStatus.Pending;

        public string Qualification { get; set; } = string.Empty;
        
        public string ExpertiseAreasJson { get; set; } = "[]"; 
        public double AverageRating { get; set; } = 0.0; 
        public int ReviewsCompleted { get; set; } = 0; 
        public bool IsAvailableForReviews { get; set; } = true;

        public ICollection<ReviewerAvailability> Availabilities { get; set; } = new List<ReviewerAvailability>();

    }
}