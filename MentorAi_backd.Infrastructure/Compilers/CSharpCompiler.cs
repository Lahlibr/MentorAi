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
    public class CSharpCompiler : ICompiler
    
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var filePath = Path.Combine(tempDir, "Program.cs");
            await File.WriteAllTextAsync(filePath, code);

            var outputPath = Path.Combine(tempDir, "Program.dll");

            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{filePath}\" -c Release -o \"{tempDir}\"",
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

            return new CompileResult { Success = true, ExecutablePath = outputPath };
        }
    }
}
