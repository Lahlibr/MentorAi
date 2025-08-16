using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using Microsoft.AspNetCore.Diagnostics;

namespace MentorAi_backd.Infrastructure.Executors
{
    public class ProcessExecutor : IExecutor
    {
        public async Task<TestCaseExecutionResult> ExecuteAsync(
            string execPath, TestCase testCase, string language, string tempDir, int memoryLimitMB,
            TimeSpan executionTimeout)

        {
            var res = new TestCaseExecutionResult
            {
                TestCaseId = testCase.Id
            };

            var (cmd, args) = GetExecutionCommand(language, execPath, tempDir);

            var psi = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = args,
                WorkingDirectory = tempDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi);
            if (proc == null)
            {
                res.Success = false;
                res.ErrorMessage = "Failed to start execution process";
                throw new ExecutionException("Failed to start process for language " + language);
            }

            if (!string.IsNullOrEmpty(testCase.Input))
            {
                await proc.StandardInput.WriteAsync(testCase.Input);
                proc.StandardInput.Close();
            }

            using var cts = new CancellationTokenSource(executionTimeout);

            try
            {
                await proc.WaitForExitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                proc.Kill(true);
                throw new ExecutionException("Time limit exceeded");
            }


            res.ExecutionTime = (int)proc.TotalProcessorTime.TotalMilliseconds;
            res.ActualOutput = (await proc.StandardOutput.ReadToEndAsync()).Trim();
            var error = (await proc.StandardError.ReadToEndAsync()).Trim();

            res.Success = proc.ExitCode == 0; // Set Success based on ExitCode

            if (!res.Success)
            {
                var errMsg = string.IsNullOrWhiteSpace(error) ? res.ActualOutput : error;
                res.ErrorMessage = errMsg;
                throw new ExecutionException($"Runtime Error: {errMsg}");
            }

            res.Passed = res.ActualOutput == testCase.ExpectedOutput.Trim();
            if (!res.Passed)
                res.ErrorMessage = "Wrong Answer";

            return res;
        }

        private (string Command, string Args) GetExecutionCommand(string language, string execPath, string tempDir) =>
            language.ToLowerInvariant() switch
            {
                "python" => ("python", $"\"{execPath}\""),
                "java" => ("java", $"-cp \"{tempDir}\" Solution"),
                "cpp" => ($"\"{execPath}\"", ""),
                "javascript" or "js" => (@"C:\Program Files\nodejs\node.exe", $"\"{execPath}\""),
                "typescript" or "ts" => ("ts-node", $"\"{execPath}\""),
                "csharp" or "cs" => ("dotnet", $"run --project \"{Path.Combine(tempDir, "Solution.csproj")}\" --no-build --no-restore"),
                _ => throw new ExecutionException($"Unsupported execution for {language}")
            };
    };
    
}
