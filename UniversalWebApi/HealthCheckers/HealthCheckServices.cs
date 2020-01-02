using Microsoft.Extensions.DependencyInjection;

using UniversalWebApi.HealthCheckers.DbCheckers;

namespace UniversalWebApi.HealthCheckers
{
    public static class HealthCheckServices
    {
        public static void AddHealthCheckServices(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<SqlChecker>("sql_database_access_check");
        }
	}
}
