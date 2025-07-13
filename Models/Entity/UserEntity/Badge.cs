using MentorAi_backd.Models.Entity.Main;
using MentorAi_backd.Models.Entity.Student;

namespace MentorAi_backd.Models.Entity.UserEntity
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
