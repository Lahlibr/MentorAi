using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class ProblemAttempt
{
    public int Id { get; set; }

    public int StudentProfileId { get; set; }

    public int ProblemId { get; set; }

    public string SubmittedCode { get; set; } = null!;

    public DateTime AttemptDate { get; set; }

    public string Status { get; set; } = null!;

    public double Score { get; set; }

    public double ExecutionTimeMs { get; set; }

    public long MemoryUsageBytes { get; set; }

    public string CompilerOutput { get; set; } = null!;

    public string TestResultsJson { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual Problem Problem { get; set; } = null!;

    public virtual Review? Review { get; set; }

    public virtual StudentProfile StudentProfile { get; set; } = null!;
}
