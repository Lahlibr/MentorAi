using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using System;

namespace MentorAi_backd.Domain.Entities.Reviewer
{
    public class ReviewerAvailability
    {
        public int Id { get; set; }

        // Foreign key
        public int ReviewerProfileId { get; set; }

        // Navigation property
        public ReviewerProfile ReviewerProfile { get; set; } = default!;

        // Enum to represent days (e.g., Monday, Tuesday)
        public DayEnum Day { get; set; }

        // Time range for availability
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Availability flag
        public bool IsAvailable { get; set; } = true;
    }
}
