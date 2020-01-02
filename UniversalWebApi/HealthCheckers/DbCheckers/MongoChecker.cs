using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace UniversalWebApi.HealthCheckers.DbCheckers
{
    public class MongoChecker : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var client = new MongoClient("Mongo");

            var database = client.GetDatabase("MongoDbName");
            
            return database != null
                ? HealthCheckResult.Healthy("MongoDb check: OK")
                : HealthCheckResult.Unhealthy("MongoDb check: Error");
        }
    }
}