using System.Linq;
using System.Text.Json;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using UniversalWebApi.HealthCheckers.DbCheckers;

namespace UniversalWebApi.HealthCheckers
{
    public static class HealthCheckServices
    {
        public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<SqlChecker>("Sql access")
                .AddCheck<MongoChecker>("MongoDb access");

            return services;
        }

        public static IApplicationBuilder AddHealthCheckBuilder(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheck
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        Duration = report.TotalDuration.ToString("g")
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });

            return app;
        }
	}
}


