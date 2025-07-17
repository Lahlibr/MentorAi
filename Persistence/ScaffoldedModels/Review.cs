using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class Review
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ReviewerId { get; set; }

    public int ProblemAttemptId { get; set; }

    public DateTime ScheduledTime { get; set; }

    public DateTime? ActualReviewStartTime { get; set; }

    public DateTime? ActualReviewEndTime { get; set; }

    public string Status { get; set; } = null!;

    public string Feedback { get; set; } = null!;

    public double? RatingGivenByStudent { get; set; }

    public string? VideoConferenceLink { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ProblemAttempt ProblemAttempt { get; set; } = null!;

    public virtual User Reviewer { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
