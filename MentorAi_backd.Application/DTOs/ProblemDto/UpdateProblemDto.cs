using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class UpdateProblemDto
    {
        [MinLength(5)]
        [MaxLength(100)]
        public string? Title { get; set; } // Nullable to allow partial updates

        [MinLength(20)]
        public string? Description { get; set; }

        public DifficultyLevelEnum? Difficulty { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? Category { get; set; }

        [MinLength(10)]
        public string? InputFormat { get; set; }

        [MinLength(10)]
        public string? OutputFormat { get; set; }

        public List<string>? ExampleInputs { get; set; }
        public List<string>? ExampleOutputs { get; set; }
        public List<string>? HiddenTestCases { get; set; }
    }
}
