using ESTADOTC.API.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace ESTADOTC.API.Infrastructure.HealthChecks;

public class DatabaseHealthCheck : IDatabaseHealthCheck
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseHealthCheck(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> IsDatabaseAvailableAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync(cancellationToken);
                return sqlConnection.State == System.Data.ConnectionState.Open;
            }

            connection.Open();

            return connection.State == System.Data.ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }
}