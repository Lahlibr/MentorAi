using MentorAi_backd.Models.Entity.Main;
using MentorAi_backd.Models.Entity.Student;

namespace MentorAi_backd.Models.Entity.UserEntity
{
    public class Certification : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
        public string CertificateUrl { get; set; } = string.Empty;
        public string Criteria { get; set; } = string.Empty; 
        public string ImageUrl { get; set; } = string.Empty; 

        public ICollection<StudentCertification> StudentCertifications { get; set; } = new List<StudentCertification>();
    }
    
    
}
