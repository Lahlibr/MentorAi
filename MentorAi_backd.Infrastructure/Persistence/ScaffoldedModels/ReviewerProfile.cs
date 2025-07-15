using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class ReviewerProfile
{
    public int UserId1 { get; set; }

    public int UserId { get; set; }

    public string Bio { get; set; } = null!;

    public int YearsOfExperience { get; set; }

    public string Availability { get; set; } = null!;

    public string ExpertiseAreasJson { get; set; } = null!;

    public double AverageRating { get; set; }

    public int ReviewsCompleted { get; set; }

    public bool IsAvailableForReviews { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual User UserId1Navigation { get; set; } = null!;
}
