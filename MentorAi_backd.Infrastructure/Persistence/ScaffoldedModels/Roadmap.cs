using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class Roadmap
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string DifficultyLevel { get; set; } = null!;

    public int EstimatedCompletionHours { get; set; }

    public int TotalModules { get; set; }

    public int TotalChallenges { get; set; }

    public int TotalCertifications { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    public virtual ICollection<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; } = new List<StudentRoadmapProgress>();
}
