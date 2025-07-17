using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class Certification
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public DateTime IssuedDate { get; set; }

    public string CertificateUrl { get; set; } = null!;

    public string Criteria { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<StudentCertification> StudentCertifications { get; set; } = new List<StudentCertification>();
}
