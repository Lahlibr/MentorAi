using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Domain.Entities.UserEntity
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
        // Foreign Key to LearningModule
        public int ModuleId { get; set; }
        public LearningModule LearningModule { get; set; } = default!;
        // Relationships
        public ICollection<ProblemAttempt> ProblemAttempts { get; set; } = new List<ProblemAttempt>();
    
   
    }
}
