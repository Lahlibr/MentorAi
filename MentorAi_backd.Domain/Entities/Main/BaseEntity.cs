namespace MentorAi_backd.Domain.Entities.Main
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;

        public bool IsDeleted { get; set; }=false;
        public bool IsActive { get; set; } = true;
        public DateTime? DeletedAt { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastLogout { get;set; }
    }
}
