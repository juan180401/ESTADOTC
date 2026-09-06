using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace ESTADOTC.API.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly IDatabaseHealthCheck _databaseHealthCheck;

    public HealthController(IDatabaseHealthCheck databaseHealthCheck)
    {
        _databaseHealthCheck = databaseHealthCheck;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealth(
        CancellationToken cancellationToken)
    {
        var databaseAvailable =
            await _databaseHealthCheck.IsDatabaseAvailableAsync(
                cancellationToken);

        var isHealthy = databaseAvailable;

        var response = new HealthStatusDto
        {
            IsHealthy = isHealthy,
            Status = isHealthy ? "UP" : "DOWN",
            TimestampUtc = DateTime.UtcNow
        };

        return isHealthy
            ? Ok(response)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
    }
}