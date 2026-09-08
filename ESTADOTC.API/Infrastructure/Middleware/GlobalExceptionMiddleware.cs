using System.Net;
using System.Text.Json;

namespace ESTADOTC.API.Infrastructure.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error durante la ejecución de la solicitud.");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = exception switch
        {
            InvalidOperationException =>
                HttpStatusCode.BadRequest,

            KeyNotFoundException =>
                HttpStatusCode.NotFound,

            _ =>
                HttpStatusCode.InternalServerError
        };

        var title = exception switch
        {
            InvalidOperationException =>
                exception.Message,

            KeyNotFoundException =>
                exception.Message,

            _ =>
                "Ocurrió un error interno en el servidor."
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = context.Response.StatusCode,
            title,
            traceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}