using System;
using System.Collections.Generic;

namespace MentorAi_backd.Infrastructure.Persistence.ScaffoldedModels;

public partial class Problem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public int DifficultyLevel { get; set; }

    public string InputFormat { get; set; } = null!;

    public string OutputFormat { get; set; } = null!;

    public string ExampleTestCasesJson { get; set; } = null!;

    public string SolutionTemplate { get; set; } = null!;

    public string ExpectedSolutionHash { get; set; } = null!;

    public int OrderInModule { get; set; }

    public int ModuleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual ICollection<ProblemAttempt> ProblemAttempts { get; set; } = new List<ProblemAttempt>();
}
