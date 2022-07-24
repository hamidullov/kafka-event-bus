using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Example.Logging.Serilog.Middlewares;

public class LoggingEnrichMiddleware : IMiddleware
{
    private readonly ILogger<LoggingEnrichMiddleware> _logger;

    public LoggingEnrichMiddleware(ILogger<LoggingEnrichMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();
        using (LogContext.PushProperty("ClientIP", !string.IsNullOrWhiteSpace(ip) ? ip : "unknown"))
        using (LogContext.PushProperty("X-Request-ID", context.Request.Headers["X-Request-ID"].FirstOrDefault()))
        {
            await next(context);
        }
    }
}