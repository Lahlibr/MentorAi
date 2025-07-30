using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class TestCaseResultDto
    {
        public int TestCaseId { get; set; }
        public bool Passed { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public string ActualOutput { get; set; }
        public string ErrorDetails { get; set; }
        public int ExecutionTime { get; set; }
        public bool IsHidden { get; set; }
    }
}
