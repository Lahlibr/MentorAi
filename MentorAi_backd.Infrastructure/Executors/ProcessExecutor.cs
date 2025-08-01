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
            var psi = new ProcessStartInfo
            {
                FileName = GetExecutionCommand(language, execPath, tempDir),
                WorkingDirectory = tempDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);
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
                proc.Kill(true); // Kill process if it times out
                throw new ExecutionException("Time limit exceeded");
            }


            res.ExecutionTime = (int)proc.TotalProcessorTime.TotalMilliseconds;
            res.ActualOutput = (await proc.StandardOutput.ReadToEndAsync()).Trim();
            var error = (await proc.StandardError.ReadToEndAsync()).Trim();

           
            if (proc.ExitCode != 0)
            {
                throw new ExecutionException($"Runtime Error: {error}");
            }

            res.Passed = res.ActualOutput == testCase.ExpectedOutput.Trim();
            if (!res.Passed)
                res.ErrorMessage = "Wrong Answer";
            return res;
        }

        private string GetExecutionCommand(string language, string execPath, string tempDir) =>
            language switch
            {
                "python" => $"python \"{execPath}\"",
                "java" => $"java -cp \"{tempDir}\" Solution",
                "cpp" => $"\"{execPath}\"",
                // ...other languages
                _ => throw new ExecutionException($"Unsupported execution for {language}")
            };
    };
}
