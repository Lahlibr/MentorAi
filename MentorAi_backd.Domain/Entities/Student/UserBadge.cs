using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.Domain.Entities.Student
{
    public class UserBadge : BaseEntity
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = default!;
        public int BadgeId { get; set; }
        public Badge Badge { get; set; } = default!;
        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;
        
    
    }
}