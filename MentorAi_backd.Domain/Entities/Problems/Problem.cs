using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Domain.Entities.Main;
using MentorAi_backd.Domain.Entities.UserEntity;
using MentorAi_backd.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class Problem : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public DifficultyLevelEnum DifficultyLevel { get; set; }

        [Required]
        public string InputFormat { get; set; }

        [Required]
        public string OutputFormat { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ExampleInputsJson { get; set; }

        [Required]
        public string ExampleOutputsJson { get; set; }

        [Required]
        public string HiddenTestCasesJson { get; set; }

        public string? ExampleTestCasesJson { get; set; }
       
        public int OrderInModule { get; set; } = 0;
        public int? ModuleId { get; set; }
        public int? RoadmapId { get; set; }
        public List<string> Tags { get; set; }

        public int TestCasesCount { get; set; }
        public virtual ICollection<ProblemLanguageSolution> LanguageSolutions { get; set; } = new List<ProblemLanguageSolution>();
        public virtual ICollection<Submission> Submission { get; set; } = new List<Submission>();
        public virtual ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public virtual LearningModule? LearningModule { get; set; }
    }
}