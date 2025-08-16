using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class CSharpCompiler : ICompiler
    {
        private static string NormalizeCode(string codeRaw)
        {
            // Remove BOM, replace non-breaking spaces, normalize all weird whitespace
            codeRaw = codeRaw.Replace('\uFEFF', ' ');  // BOM
            codeRaw = codeRaw.Replace('\u00A0', ' ');  // Non-breaking space
            codeRaw = System.Text.RegularExpressions.Regex.Replace(codeRaw, @"[^\S\r\n]+", " ");
            codeRaw = codeRaw.Replace("\r\n", "\n").Replace("\r", "\n");
            return codeRaw.Trim();
        }
        private static string WrapWithMain(string userCode)
        {
            // If user code already has Main method, return as-is
            if (userCode.Contains("static void Main") || userCode.Contains("static int Main") ||
                userCode.Contains("static async Task Main"))
            {
                return userCode;
            }

            // Otherwise, wrap it with a Main method
            return $@"using System;
using System.Collections.Generic;
using System.Linq;

{userCode}

public class Program 
{{
    public static void Main(string[] args) 
    {{
        Solution solution = new Solution();
        solution.Solve();
    }}
}}";
        }


        public async Task<CompileResult> CompileAsync(string code, string workingDirectory)
        {
            try
            {
                // Ensure working directory exists
                if (!Directory.Exists(workingDirectory))
                {
                    Directory.CreateDirectory(workingDirectory);
                }

                // Normalize the user's code BEFORE saving
                var normalizedCode = NormalizeCode(code);
                var wrappedCode = WrapWithMain(normalizedCode);

                // Write the normalized code to a .cs file
                var fileName = Path.Combine(workingDirectory, "Solution.cs");
                await File.WriteAllTextAsync(fileName, normalizedCode, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

                // Verify the file was written correctly
                if (!File.Exists(fileName))
                {
                    return new CompileResult { Success = false, Error = "Failed to write source file" };
                }

                // Create a minimal csproj file
                var csprojContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
</Project>";

                var csprojPath = Path.Combine(workingDirectory, "Solution.csproj");
                await File.WriteAllTextAsync(csprojPath, csprojContent, Encoding.UTF8);

                // Run dotnet build
                var psi = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build \"{csprojPath}\" --configuration Release --output \"{workingDirectory}\"",
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null)
                {
                    return new CompileResult { Success = false, Error = "Failed to start dotnet build process" };
                }

                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    var fullError = $"Build failed with exit code {process.ExitCode}\nSTDERR: {error}\nSTDOUT: {output}";
                    return new CompileResult { Success = false, Error = fullError };
                }

                // Look for the compiled executable
                var exePath = Path.Combine(workingDirectory, "Solution.exe");
                if (!File.Exists(exePath))
                {
                    return new CompileResult { Success = false, Error = "Compilation succeeded but executable not found at " + exePath };
                }

                return new CompileResult
                {
                    Success = true,
                    ExecutablePath = exePath,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new CompileResult { Success = false, Error = $"Compilation exception: {ex.Message}" };
            }
        }
    }
}
