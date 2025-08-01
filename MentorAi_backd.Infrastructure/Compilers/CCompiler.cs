using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class CCompiler : ICompiler
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var filePath = Path.Combine(tempDir, "solution.c");
            await File.WriteAllTextAsync(filePath, code);

            var exePath = Path.Combine(tempDir, "solution");
            var psi = new ProcessStartInfo
            {
                FileName = "gcc",
                Arguments = $"\"{filePath}\" -o \"{exePath}\"",
                WorkingDirectory = tempDir,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                return new CompileResult { Success = false, Error = error };
            }

            return new CompileResult { Success = true, ExecutablePath = exePath };
        }
    }
}
