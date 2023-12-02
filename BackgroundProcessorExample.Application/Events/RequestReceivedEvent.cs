using MediatR;
using Microsoft.Extensions.Logging;

namespace BackgroundProcessorExample.Application.Events
{
    public record RequestReceivedEvent : INotification
    {
        public string WhoYouGonnaCall { get; set; }

        public RequestReceivedEvent(string whoYouGonnaCall)
        {
            WhoYouGonnaCall = whoYouGonnaCall;
        }
    }

    public class RequestReceivedEventHandler : INotificationHandler<RequestReceivedEvent>
    {
        public ILogger<RequestReceivedEventHandler> _logger { get; set; }

        public RequestReceivedEventHandler(ILogger<RequestReceivedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(RequestReceivedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Who you gonna call?");
            await Task.Delay(2000);
            _logger.LogWarning(notification.WhoYouGonnaCall);
        }
    }
}
