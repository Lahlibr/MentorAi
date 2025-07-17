using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class StudentProfile
{
    public int UserId1 { get; set; }

    public int UserId { get; set; }

    public int? Age { get; set; }

    public int? AssessmentScore { get; set; }

    public string? CurrentLearningGoal { get; set; }

    public string? PreferredLearningStyle { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<ProblemAttempt> ProblemAttempts { get; set; } = new List<ProblemAttempt>();

    public virtual ICollection<StudentCertification> StudentCertifications { get; set; } = new List<StudentCertification>();

    public virtual ICollection<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; } = new List<StudentRoadmapProgress>();

    public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

    public virtual User UserId1Navigation { get; set; } = null!;
}
