using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class UserBadge
{
    public int Id { get; set; }

    public int StudentProfileId { get; set; }

    public int BadgeId { get; set; }

    public DateTime AwardedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual Badge Badge { get; set; } = null!;

    public virtual StudentProfile StudentProfile { get; set; } = null!;
}
