using BackgroundProcessorExample.Application.Events;
using BackgroundProcessorExample.Domain.BackgroundProcessor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundProcessorExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundTaskQueue _bus;

        public ProcessController(ILogger<WeatherForecastController> logger, IBackgroundTaskQueue bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _bus.QueueTaskAsync(new RequestReceivedEvent("GhostBusters!"), default);
            return Ok("IDK lol");
        }
    }
}
