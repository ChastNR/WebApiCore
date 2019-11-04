using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.BackgroundServices
{
    public static class HostedServices
    {
        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<ActionTelemetryCollector>();
        }
    }
    
    public class ActionTelemetryCollector : BackgroundService
    {
        readonly ILogger<ActionTelemetryCollector> _logger;

        public ActionTelemetryCollector(ILogger<ActionTelemetryCollector> logger)
        {
            _logger = logger;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}