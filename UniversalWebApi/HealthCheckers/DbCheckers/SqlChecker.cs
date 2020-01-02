using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace UniversalWebApi.HealthCheckers.DbCheckers
{
    public class SqlChecker : IHealthCheck
    {
        private readonly string _testQuery;

        public SqlChecker()
        {
            _testQuery = "SELECT db_name()";
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection("Sql"))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    
                    var command = connection.CreateCommand();
                    
                    command.CommandText = _testQuery;

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }
}
