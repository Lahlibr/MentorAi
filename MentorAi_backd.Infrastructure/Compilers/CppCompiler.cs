using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.Common.Exceptions;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class CppCompiler : ICompiler
    {
        public async Task<CompileResult> CompileAsync(string code, string tempDir)
        {
            var sourceFile = Path.Combine(tempDir, "solution.cpp");
            var outputFile = Path.Combine(tempDir, "solution.out");

            // Save the source code to a file
            await File.WriteAllTextAsync(sourceFile, code);

            var psi = new ProcessStartInfo
            {
                FileName = "g++",
                Arguments = $"-o \"{outputFile}\" \"{sourceFile}\"",
                WorkingDirectory = tempDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            var error = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            if (proc.ExitCode != 0)
            {
                throw new CompilationException(error);
            }

            return new CompileResult
            {
                Success = true,
                ExecutablePath = outputFile
            };
        }
    }
}
