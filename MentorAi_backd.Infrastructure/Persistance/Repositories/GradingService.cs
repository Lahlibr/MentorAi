using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Domain.Enums;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    //It's a background worker (based on BackgroundService) that runs in the background of your application and:

    // Listens for new grading jobs via a Channel queue.

    // Processes each submission by:

    //     Running the code against test cases.

    //     Saving the results to the database.

    //     Sending real-time updates to the frontend using SignalR.
    public class GradingService : BackgroundService
    {
        private readonly IBackgroundJobQueue _jobQueue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<GradingService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GradingService(IBackgroundJobQueue jobQueue, IServiceScopeFactory scopeFactory, ILogger<GradingService> logger,UnitOfWork unitOfWork)
        {
            _jobQueue = jobQueue;
            _scopeFactory = scopeFactory;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var submissionId = await _jobQueue.DequeueAsync(stoppingToken);
                    await ProcessSubmissionAsync(submissionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing grading job");
                }
            }
        }

        private async Task ProcessSubmissionAsync(int submissionId)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MentorAiDbContext>();
            var codeRunner = scope.ServiceProvider.GetRequiredService<ICodeRunnerService>();
            var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<SubmissionStatus>>();

            try
            {
                var submission = await context.Submissions
                    .Include(s => s.Problem)
                    .ThenInclude(p => p.TestCases)
                    .FirstOrDefaultAsync(s => s.Id == submissionId);
                if (submission == null)
                {
                    _logger.LogWarning("Submission with ID {SubmissionId} not found", submissionId);
                    return;
                }

                var executionResult = await codeRunner.ExecuteCodeAsync(
                    submission.Code,
                    submission.Language,
                    submission.Problem.TestCases.ToList());

                submission.Status = "Graded";
                submission.ExecutionTime = executionResult.TotalExecutionTime;
                submission.MemoryUsed = executionResult.MemoryUsed;

                if (!executionResult.CompileSuccess)
                {
                    submission.CompileError = executionResult.CompileError;
                    submission.IsCorrect = false;
                }
                else
                {
                    submission.IsCorrect = executionResult.TestResults.All(tr => tr.IsPassed);
                }
                foreach (var testResult in executionResult.TestResults)
                {
                    var testCaseResult = new TestCaseExecutionResult
                    {
                        SubmissionId = submissionId,
                        TestCaseId = testResult.TestCaseId,
                        Passed = testResult.IsPassed,
                        ActualOutput = testResult.ActualOutput,
                        ErrorMessage = testResult.ErrorMessage,
                        ExecutionTime = testResult.ExecutionTime
                    };
                    context.TestCaseResults.Add(testCaseResult);
                    await _unitOfWork .SaveChangesAsync();
                    
                    var resultDto = new TestCaseResultDto
                    {
                        SubmissionId = submissionId,
                        Status = submission.Status,
                        IsCorrect = submission.IsCorrect,
                        CompileError = submission.CompileError,
                        ExecutionTime = submission.ExecutionTime,
                        MemoryUsed = submission.MemoryUsed,
                        TestCaseResults = executionResult.TestResults.Select(tr => new TestCaseResultDto()
                        {
                            TestCaseId = tr.TestCaseId,
                            Passed = tr.Passed,
                            ActualOutput = tr.ActualOutput,
                            ErrorDetails = tr.ErrorMessage,
                            ExecutionTime = tr.ExecutionTime
                        }).ToList()
                    };
                    await hubContext.Clients.Group($"submission_{submissionId}")
                        .SendAsync("ReceiveSubmissionResult", resultDto);
                    _logger.LogInformation("Processed submission {SubmissionId} with status {Status}", submissionId, submission.Status);

                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing submission {SubmissionId}", submissionId);
                var submission = await context.Submissions.FindAsync(submissionId);
                if (submission != null)
                {
                    submission.Status = "Error";
                    submission.CompileError = $"System error:{ex.Message}";
                    await _unitOfWork.SaveChangesAsync();
                    
                    var errorDto = new TestCaseResultDto
                    {
                        SubmissionId = submissionId,
                        Status = submission.Status,
                        CompileError = ex.Message
                    };
                    await hubContext.Clients.Group($"submission_{submissionId}")
                        .SendAsync("ReceiveSubmissionResult", errorDto);
                }
            }

        }
    }
}
