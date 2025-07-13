using MentorAi_backd.Models.Entity.Main;
using MentorAi_backd.Models.Entity.Student;
using MentorAi_backd.Models.Enum;

namespace MentorAi_backd.Models.Entity.UserEntity
{
    public class Problem : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public  DifficultyLevelEnum DifficultyLevel { get; set; }
        public string InputFormat { get; set; } = string.Empty;
        public string OutputFormat { get; set; } = string.Empty;
        public string ExampleTestCasesJson { get; set; } = "[]"; 
        public string SolutionTemplate { get; set; } = string.Empty; 
        public string ExpectedSolutionHash { get; set; } = string.Empty; 
        public int OrderInModule { get; set; } = 0;
        // Foreign Key to Module
        public int ModuleId { get; set; }
        public Module Module { get; set; } = default!;
        // Relationships
        public ICollection<ProblemAttempt> ProblemAttempts { get; set; } = new List<ProblemAttempt>();
    
   
    }
}
