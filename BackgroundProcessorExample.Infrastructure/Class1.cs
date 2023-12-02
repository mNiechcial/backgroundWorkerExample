using BackgroundProcessorExample.Domain.BackgroundProcessor;
using BackgroundProcessorExample.Infrastructure.BackgroundProcessor;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace BackgroundProcessorExample.Infrastructure
{
    public static class InfrastruicutreExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {

            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<CustomBackgroundService>();
        }

    }
}
