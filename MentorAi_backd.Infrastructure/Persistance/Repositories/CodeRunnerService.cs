// Infrastructure/Persistence/Repositories/CodeRunnerService.cs

using MentorAi_backd.Application.Common.Exceptions;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Infrastructure.Compilers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MentorAi_backd.Infrastructure.Persistence.Repositories
{
    public class CodeRunnerService : ICodeRunnerService
    {
        private readonly CompilerFactory _compilerFactory;
        private readonly IExecutor _executor;
        private readonly ILogger<CodeRunnerService> _logger;
        private readonly IConfiguration _config;
        private readonly TimeSpan _compilationTimeout;
        private readonly TimeSpan _executionTimeout;
        private readonly int _memoryLimitMB;

        public CodeRunnerService(
            ILogger<CodeRunnerService> logger,
            CompilerFactory compilerFactory,
            IExecutor executor,
            IConfiguration config)
        {
            _compilerFactory = compilerFactory;
            _executor = executor;
            _logger = logger;
            _config = config;
            _compilationTimeout = TimeSpan.FromSeconds(_config.GetValue<int>("CodeExecution:CompilationTimeoutSeconds", 10));
            _executionTimeout = TimeSpan.FromSeconds(_config.GetValue<int>("CodeExecution:ExecutionTimeoutSeconds", 5));
            _memoryLimitMB = _config.GetValue<int>("CodeExecution:MemoryLimitMB", 256);
        }

        
        public async Task<ApiResponse<CodeExecutionResult>> ExecuteCodeAsync(
            string code, string language, List<TestCase> testCases)
        {
            var result = await ExecuteInternalAsync(code, language, testCases);
            return ApiResponse<CodeExecutionResult>.SuccessResponse(result);
        }

       
        public async Task<ApiResponse<CodeExecutionResultDto>> ExecuteSimpleCodeAsync(
            string code, string language, string input = "")
        {
            try
            {
                var testCases = new List<TestCase>
                {
                    new TestCase
                    {
                        Input = input ?? "",
                        ExpectedOutput = "",
                        IsHidden = false
                    }
                };

                var result = await ExecuteInternalAsync(code, language, testCases);

                var dto = new CodeExecutionResultDto
                {
                    Success = result.CompileSuccess && result.TestResults.All(t => t.Success),
                    Output = result.TestResults.FirstOrDefault()?.ActualOutput ?? "",
                    Error = result.TestResults.FirstOrDefault()?.ErrorMessage ??
                            result.TestResults.FirstOrDefault()?.Error ??
                            (result.CompileSuccess ? "" : "Compilation failed"),
                    ExecutionTime = (int)result.TotalExecutionTime,
                    MemoryUsed = result.TestResults.FirstOrDefault()?.MemoryUsed ?? 0
                };

                return ApiResponse<CodeExecutionResultDto>.SuccessResponse(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Simple code execution failed");
                return ApiResponse<CodeExecutionResultDto>.ErrorResponse(
                    ex.Message, null, 500);
            }
        }

        
        private async Task<CodeExecutionResult> ExecuteInternalAsync(
            string code, string language, List<TestCase> testCases)
        {
            var result = new CodeExecutionResult();
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                var compiler = _compilerFactory.GetCompiler(language);
                if (compiler == null)
                {
                    throw new CompilationException($"Unsupported language: {language}");
                }

                var compileResult = await compiler.CompileAsync(code, tempDir);
                if (!compileResult.Success)
                {
                    result.CompileSuccess = false;
                    result.TestResults.Add(new TestCaseExecutionResult
                    {
                        Success = false,
                        Error = compileResult.Error,
                        ExecutionTime = 0,
                        MemoryUsed = 0
                    });
                    return result;
                }

                result.CompileSuccess = true;

                foreach (var testCase in testCases)
                {
                    var testRes = await _executor.ExecuteAsync(
                        compileResult.ExecutablePath,
                        testCase,
                        language,
                        tempDir,
                        _memoryLimitMB,
                        _executionTimeout
                    );

                    result.TestResults.Add(testRes);
                    result.TotalExecutionTime += testRes.ExecutionTime;
                }

                return result;
            }
            catch (CompilationException ce)
            {
                _logger.LogError(ce, "Compilation failed");
                result.CompileSuccess = false;
                result.TestResults.Add(new TestCaseExecutionResult
                {
                    Success = false,
                    Error = $"Compilation Error: {ce.Message}",
                    ExecutionTime = 0,
                    MemoryUsed = 0
                });
                return result;
            }
            catch (ExecutionException ee)
            {
                _logger.LogError(ee, "Execution failed");
                result.TestResults.Add(new TestCaseExecutionResult
                {
                    Success = false,
                    ErrorMessage = $"Execution Error: {ee.Message}",
                    ExecutionTime = 0,
                    MemoryUsed = 0
                });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal error");
                result.TestResults.Add(new TestCaseExecutionResult
                {
                    Success = false,
                    ErrorMessage = "Internal Server Error: " + ex.Message,
                    ExecutionTime = 0,
                    MemoryUsed = 0
                });
                return result;
            }

            finally
            {
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                }
            }

        }

    }
}
