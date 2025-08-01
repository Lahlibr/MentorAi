using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IExecutor
    {
        Task<TestCaseExecutionResult> ExecuteAsync(
            string executablePath,
            TestCase testCase,
            string language,
            string tempDir,
            int memoryLimitMB,
            TimeSpan executionTimeout
        );
    }

}
