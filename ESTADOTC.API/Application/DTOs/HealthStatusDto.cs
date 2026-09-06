namespace ESTADOTC.API.Application.DTOs;

public class HealthStatusDto
{
    public bool IsHealthy { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
}