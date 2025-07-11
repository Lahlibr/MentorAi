namespace MentorAi_backd.Models.Entity
{
    public class StudentCertification : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;
       
        public int CertificationId { get; set; }
        public Certification Certification { get; set; } = default!;
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // e.g., "Pending", "Issued", "Revoked"
    }
    
    
}