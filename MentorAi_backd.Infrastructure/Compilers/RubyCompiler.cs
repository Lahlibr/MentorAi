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
    public class RubyCompiler : ICompiler
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var path = Path.Combine(tempDir, "script.rb");
            await File.WriteAllTextAsync(path, code);
            return new CompileResult
            {
                Success = true,
                ExecutablePath = path
            };
        }
    }
}
