using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class StudentRoadmapProgress
{
    public int Id { get; set; }

    public int StudentProfileId { get; set; }

    public int RoadmapId { get; set; }

    public double CurrentProgressPercentage { get; set; }

    public int? CurrentModuleId { get; set; }

    public DateTime? CompletionDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public virtual Module? CurrentModule { get; set; }

    public virtual Roadmap Roadmap { get; set; } = null!;

    public virtual StudentProfile StudentProfile { get; set; } = null!;
}
