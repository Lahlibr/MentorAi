using MentorAi_backd.Models.Entity.Main;
using MentorAi_backd.Models.Entity.UserEntity;

namespace MentorAi_backd.Models.Entity.Student
{
    public class ProblemAttempt : BaseEntity
    {
        public int Id { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; } = default!;

        public int ProblemId { get; set; }
        public Problem Problem { get; set; } = default!;

        public string SubmittedCode { get; set; } = string.Empty;
        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // e.g., "Passed", "Failed", "Pending", "Error"
        public double Score { get; set; } = 0.0; 
        public double ExecutionTimeMs { get; set; } = 0.0; 
        public long MemoryUsageBytes { get; set; } = 0; 
        public string CompilerOutput { get; set; } = string.Empty;
        public string TestResultsJson { get; set; } = "[]"; 
        
        
        public Review? Review { get; set; }
    }
}