using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class Module
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int OrderInRoadmap { get; set; }

    public int RoadmapId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();

    public virtual Roadmap Roadmap { get; set; } = null!;

    public virtual ICollection<StudentRoadmapProgress> StudentRoadmapProgresses { get; set; } = new List<StudentRoadmapProgress>();
}
