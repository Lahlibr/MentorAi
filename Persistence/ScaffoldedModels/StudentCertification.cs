using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class StudentCertification
{
    public int Id { get; set; }

    public int StudentProfileId { get; set; }

    public int CertificationId { get; set; }

    public DateTime IssuedDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual Certification Certification { get; set; } = null!;

    public virtual StudentProfile StudentProfile { get; set; } = null!;
}
