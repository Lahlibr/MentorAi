using MentorAi_backd.Models.Entity.Main;
using MentorAi_backd.Models.Entity.UserEntity;

namespace MentorAi_backd.Models.Entity.Student
{
    public class StudentCertification : BaseEntity
    {
        public int Id { get; set; }
        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = default!;

        public int CertificationId { get; set; }
        public Certification Certification { get; set; } = default!;
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // e.g., "Pending", "Issued", "Revoked"
    }
    
    
}