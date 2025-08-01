using MentorAi_backd.Application.Interfaces;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class PythonCompiler : ICompiler
    {
        public Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var path = Path.Combine(tempDir, "solution.py");
            File.WriteAllText(path,code);
            return Task.FromResult(new CompileResult
            {
                Success = true,
                ExecutablePath = path
            });
        }

    }
}
