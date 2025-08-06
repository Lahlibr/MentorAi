using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class ProblemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DifficultyLevelEnum Difficulty { get; set; }
        public string Category { get; set; }
        public string InputFormat { get; set; }
        public string OutputFormat { get; set; }
        public List<string> ExampleInputs { get; set; } = new List<string>();
        public List<string> ExampleOutputs { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<TestCaseResultDto> SampleTestCases { get; set; } = new();
    }
}
