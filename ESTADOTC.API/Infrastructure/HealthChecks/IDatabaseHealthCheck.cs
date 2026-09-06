namespace ESTADOTC.API.Infrastructure.HealthChecks;

public interface IDatabaseHealthCheck
{
    Task<bool> IsDatabaseAvailableAsync(
        CancellationToken cancellationToken);
}