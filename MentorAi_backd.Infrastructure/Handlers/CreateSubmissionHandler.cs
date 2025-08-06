using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MentorAi_backd.Application.Commands;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;
using MentorAi_backd.Infrastructure.Persistance.Data;

namespace MentorAi_backd.Infrastructure.Handlers
{
    public class CreateSubmissionHandler : IRequestHandler<CreateSubmissionCommand, int>
    {
        private readonly MentorAiDbContext _context;
        private readonly IBackgroundJobQueue _jobQueue;
        public CreateSubmissionHandler(MentorAiDbContext context, IBackgroundJobQueue jobQueue)
        {
            _context = context;
            _jobQueue = jobQueue;
        }
        public async Task<int> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            var submission = new Submission
            {
                ProblemId = request.ProblemId, 
                StudentId = request.UserId,
                Code = request.Code,
                Language = request.Language,
                
            };
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync(cancellationToken);
            // Enqueue the grading job
            await _jobQueue.EnqueueGradingJobAsync(submission.Id);
            return submission.Id;
        }
    }
}
