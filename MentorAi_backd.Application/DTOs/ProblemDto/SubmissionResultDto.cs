using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class SubmissionResultDto
    {
        public int SubmissionId { get; set; }
        public string Status { get; set; }
        public bool IsCorrect { get; set; }
        public string CompileError { get; set; }
        public int ExecutionTime { get; set; }
        public int MemoryUsed { get; set; }
        public List<TestCaseResultDto> TestCases { get; set; } = new List<TestCaseResultDto>();
    }
}
