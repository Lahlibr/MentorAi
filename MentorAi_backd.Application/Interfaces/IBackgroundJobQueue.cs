using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IBackgroundJobQueue
    {
        Task EnqueueGradingJobAsync(int submissionId);
        Task<int> DequeueAsync(CancellationToken cancellationToken);
    }
}
