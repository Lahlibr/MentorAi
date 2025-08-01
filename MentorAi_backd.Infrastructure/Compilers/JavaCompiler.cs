using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.Interfaces;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class JavaCompiler : ICompiler
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var javaFile = Path.Combine(tempDir, "Solution.java");
            await File.WriteAllTextAsync(javaFile, code);

            var psi = new ProcessStartInfo
            {
                FileName = "javac",
                Arguments = $"\"{javaFile}\"", 
                WorkingDirectory = tempDir,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);
            var error = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();
            if (proc.ExitCode != 0) throw new CompilationException(error);

            return new CompileResult
            {
                Success = true,
                ExecutablePath = Path.Combine(tempDir, "Solution.class")
            };


        }
    }
}
