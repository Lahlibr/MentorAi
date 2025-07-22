using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using System;

namespace MentorAi_backd.Domain.Entities.Reviewer
{
    public class ReviewerAvailability
    {
        public int Id { get; set; }

   
        public int ReviewerProfileId { get; set; }

       
        public ReviewerProfile ReviewerProfile { get; set; } = default!;

       
        public DayEnum Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public bool IsAvailable { get; set; } = true;
    }
}
