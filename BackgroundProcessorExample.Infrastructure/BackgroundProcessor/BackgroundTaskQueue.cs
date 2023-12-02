using BackgroundProcessorExample.Domain.BackgroundProcessor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BackgroundProcessorExample.Infrastructure.BackgroundProcessor
{
    internal class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<INotification> _queue = Channel.CreateUnbounded<INotification>();

        public async Task QueueTaskAsync(INotification task, CancellationToken cancellationToken = default)
        {
            await _queue.Writer.WriteAsync(task, cancellationToken);
        }

        public async Task<INotification> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
