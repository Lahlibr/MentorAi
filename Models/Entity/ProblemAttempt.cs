namespace MentorAi_backd.Models.Entity
{
    public class ProblemAttempt : BaseEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProblemId { get; set; }
        public Problem Problem { get; set; }

        public string SubmittedCode { get; set; } = string.Empty;
        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // e.g., "Passed", "Failed", "Pending", "Error"
        public double Score { get; set; } = 0.0; // Score for this attempt (e.g., 0-100)
        public double ExecutionTimeMs { get; set; } = 0.0; // In milliseconds
        public long MemoryUsageBytes { get; set; } = 0; // In bytes
        public string CompilerOutput { get; set; } = string.Empty; // Output from compiler/runner
        public string TestResultsJson { get; set; } = "[]"; // Detailed test case results (JSON string)

        // Foreign Key to Review (if an attempt is reviewed)
        public int? ReviewId { get; set; }
        public Review? Review { get; set; } // Navigation proper
    }
}