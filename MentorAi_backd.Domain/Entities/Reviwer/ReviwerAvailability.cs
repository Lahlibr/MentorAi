using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Reviwer
{
    public class ReviwerAvailability
    {
        public int Id { get; set; }
        public int ReviewerProfileId { get; set; }
        public ReviewerProfile ReviewerProfile { get; set; } = default!; 
        public DayEnum dayEnum { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        
        
    }
}
