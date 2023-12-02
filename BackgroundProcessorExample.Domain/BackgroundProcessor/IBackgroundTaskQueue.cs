using MediatR;

namespace BackgroundProcessorExample.Domain.BackgroundProcessor
{
    public interface IBackgroundTaskQueue
    {
        Task<INotification> DequeueAsync(CancellationToken cancellationToken);
        Task QueueTaskAsync(INotification task, CancellationToken cancellationToken);
    }
}
