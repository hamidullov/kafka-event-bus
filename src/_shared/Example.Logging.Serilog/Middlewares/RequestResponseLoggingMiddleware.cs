using System.Text;
using Example.Logging.Serilog.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Example.Logging.Serilog.Middlewares;

public class RequestResponseLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var body = (await ReadRequestBody(context.Request)).Truncate(1000);
        using (LogContext.PushProperty("UserName", context.User?.Identity.Name))
        using (LogContext.PushProperty("HttpBody", body.SanitizingLog()))
        {
            _logger.LogInformation("Request [{HttpMethod}]: {RequestPath}", context.Request.Method,
                context.Request.Path);
            await next(context);
        }
    }

    private static async Task<string> ReadRequestBody(HttpRequest request)
    {
        if (request.Method is not ("POST" or "PUT")) return "";
        
        var requestStream = new MemoryStream();
        if (request.Body.CanSeek)
            request.Body.Position = 0;
        await request.Body.CopyToAsync(requestStream);

        requestStream.Position = 0;
        request.Body = requestStream;

        return Encoding.UTF8.GetString(requestStream.ToArray());

    }
}