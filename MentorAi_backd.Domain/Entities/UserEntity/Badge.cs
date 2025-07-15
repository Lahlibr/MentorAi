using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Student;

namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class Badge : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        
        public string Criteria { get; set; } = string.Empty;
        // Relationships
        public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
    }
   
}
