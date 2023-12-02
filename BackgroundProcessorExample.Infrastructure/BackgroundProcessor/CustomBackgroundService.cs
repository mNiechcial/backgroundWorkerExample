using BackgroundProcessorExample.Domain.BackgroundProcessor;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundProcessorExample.Infrastructure.BackgroundProcessor
{
    internal class CustomBackgroundService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<CustomBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public CustomBackgroundService(IBackgroundTaskQueue queue, ILogger<CustomBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _queue = queue;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("!!!!!!!!!!!!!!!!!!!Service {serviceName} is running", nameof(CustomBackgroundService));
            return ProcessTaskQueueAsync(cancellationToken);


        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{serviceName} is stopping listening", nameof(CustomBackgroundService));
        }

        private async Task ProcessTaskQueueAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Waiting for a new queue message");
                    var backgroundTask = await _queue.DequeueAsync(cancellationToken);

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                        _logger.LogInformation("Running task of type {taskType}", backgroundTask.GetType());
                        await publisher.Publish(backgroundTask, cancellationToken);
                        _logger.LogInformation("Completed task {taskType}", backgroundTask.GetType());
                    }
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogWarning(ex, "Background processor was cancelled");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception was thrown");
                }
            }
        }
    }
}
