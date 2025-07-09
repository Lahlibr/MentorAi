namespace MentorAi_backd.Models.Entity
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;

        public bool IsDeleted { get; set; }=false;
        public DateTime? DeletedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
