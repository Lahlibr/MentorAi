using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class CodeExecutionResult
    {
        public bool CompileSuccess { get; set; }
        public string CompileError { get; set; }
        public List<TestCaseExecutionResult> TestResults { get; set; } = new();
        public int TotalExecutionTime { get; set; }
        public int MemoryUsed { get; set; }
    }
}
