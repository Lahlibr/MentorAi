using System.Threading.Channels;
using MentorAi_backd.Application.Interfaces;

public class BackgroundJobQueue : IBackgroundJobQueue
{
    private readonly Channel<int> _queue;

    public BackgroundJobQueue()
    {
        var options = new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = false,
            SingleWriter = false
        };

        _queue = Channel.CreateBounded<int>(options);
    }

    public async Task EnqueueGradingJobAsync(int submissionId)
    {
        await _queue.Writer.WriteAsync(submissionId);
    }

    public async Task<int> DequeueAsync(CancellationToken cancellationToken)
    {
        var result = await _queue.Reader.ReadAsync(cancellationToken);
        return result;
    }
}