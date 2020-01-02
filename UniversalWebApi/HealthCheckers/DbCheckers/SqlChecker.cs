using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UniversalWebApi.HealthCheckers.DbCheckers
{
    public class SqlChecker : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool sqlHealth;

            using(var connection = new SqlConnection("Sql"))
            {
                sqlHealth = connection.State == ConnectionState.Open;
            }

            return sqlHealth
                ? HealthCheckResult.Healthy("Sql check: OK")
                : HealthCheckResult.Unhealthy("Sql check: Error");
        }
    }
}
