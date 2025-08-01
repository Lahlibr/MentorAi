using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Infrastructure.Compilers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MentorAi_backd.Application.Common.Exceptions;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
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

        public CodeRunnerService(ILogger<CodeRunnerService> logger,
        CompilerFactory compilerFactory, IExecutor executor,
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

        private async Task<CodeExecutionResult> ExecuteInternalAsync(
            string code, string language, List<TestCase> testCases)
        {
            var result = new CodeExecutionResult();
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            try
            {
                var compiler = _compilerFactory.GetCompiler(language);
                var compileResult = await compiler.CompileAsync(code, tempDir);
                if (!compileResult.Success)
                    throw new CompilationException(compileResult.Error);

                result.CompileSuccess = true;

                foreach (var testCase in testCases)
                {
                    var testRes = await _executor.ExecuteAsync(
                        compileResult.ExecutablePath, testCase, language, tempDir, _memoryLimitMB, _executionTimeout);
                    result.TestResults.Add(testRes);
                    result.TotalExecutionTime += testRes.ExecutionTime;
                }
                return result;
            }
            catch (CompilationException ce)
            {
                _logger.LogError(ce, "Compilation failed");
                throw new Exception($"Compilation Error: {ce.Message}");
            }
            catch (ExecutionException ee)
            {
                _logger.LogError(ee, "Execution failed");
                throw new Exception($"Execution Error: {ee.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal error");
                throw new Exception("Internal Server Error");
            }

            finally
            {
                try { Directory.Delete(tempDir, true); }
                catch { }
            }

        }

    }
}
