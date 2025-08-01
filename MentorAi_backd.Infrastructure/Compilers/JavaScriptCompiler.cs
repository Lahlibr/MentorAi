using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class JavaScriptCompiler : ICompiler
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var filePath = Path.Combine(tempDir, "script.js");
            await File.WriteAllTextAsync(filePath, code);

            return new CompileResult
            {
                Success = true,
                ExecutablePath = filePath
            };
        }
    }
}
