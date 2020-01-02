using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Tools.Messages;

using UniversalWebApi.HealthCheckers;

namespace UniversalWebApi.BackgroundServices
{
    public class ApiHealthHostedService : IHostedService, IDisposable
    {
        private const string Unhealthy = "Unhealthy";
        private readonly ILogger<ApiHealthHostedService> _logger;
        private Timer _timer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageSender _sender;

        public ApiHealthHostedService(ILogger<ApiHealthHostedService> logger, IHttpClientFactory httpClientFactory, IMessageSender sender)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _sender = sender;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ApiHealthHostedService)} is running.");

            _timer = new Timer(async o => await DoWork(), null, TimeSpan.Zero, 
                TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private async Task DoWork()
        {            
            try
            {
                var result = await _httpClientFactory.CreateClient().GetAsync("http://localhost:5000/health");
                    
                var jsonString = await result.Content.ReadAsStringAsync();
                    
                var response = JsonSerializer.Deserialize<HealthCheckResponse>(jsonString);

                var brokenServices = response.Checks.Where(s => s.Status == Unhealthy).ToList();

                if (brokenServices.Any())
                {
                    var message = BuildMessage(brokenServices);

                    // await _sender.SendServiceMessageAsync(new ServiceMessage
                                              // {
                                              //     Title = "API services errors",
                                              //     Body = message
                                              // });
                        
                    _logger.LogCritical(message);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogCritical($"{nameof(HttpRequestException)}: {ex.Message}");
            }
        }

        private string BuildMessage(IEnumerable<HealthCheck> response)
        {
            var message = new StringBuilder();

            foreach (var check in response)
            {
                message.Append($"{check.Component}: {check.Status}, {check.Description}\n");
            }

            return message.ToString();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}