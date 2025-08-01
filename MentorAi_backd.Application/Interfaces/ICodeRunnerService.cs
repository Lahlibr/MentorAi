using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Application.Interfaces
{
    public interface ICodeRunnerService
    {
        Task<ApiResponse<CodeExecutionResult>>ExecuteCodeAsync(string code, string language, List<TestCase>testCases);
    }
}
