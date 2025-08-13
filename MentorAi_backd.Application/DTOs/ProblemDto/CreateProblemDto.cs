using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class CreateProblemDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public DifficultyLevelEnum Difficulty { get; set; } = DifficultyLevelEnum.Easy;

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Category { get; set; }

        [Required]
        [MinLength(10)]
        public string InputFormat { get; set; }

        [Required]
        [MinLength(10)]
        public string OutputFormat { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one example input is required.")]
        public List<string> ExampleInputs { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one example output is required.")]
        public List<string> ExampleOutputs { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "At least one tag is required.")]
        public List<string> Tags { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one hidden test case is required.")]
        public List<string> HiddenTestCases { get; set; }

        

        [Required]
    [MinLength(1, ErrorMessage = "At least one language solution is required.")]
    public List<LanguageSolutionDto> LanguageSolutions { get; set; }
    }
}