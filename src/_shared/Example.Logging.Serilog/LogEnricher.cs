using Microsoft.AspNetCore.Http;
using Serilog;

namespace Example.Logging.Serilog;

static class LogEnricher
{
    /// <summary>
    /// Enriches the HTTP request log with additional data via the Diagnostic Context
    /// </summary>
    /// <param name="diagnosticContext">The Serilog diagnostic context</param>
    /// <param name="httpContext">The current HTTP Context</param>
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        try
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            diagnosticContext.Set("ClientIP", !string.IsNullOrWhiteSpace(ip) ? ip : "unknown");
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("X-Request-ID", httpContext.Request.Headers["X-Request-ID"].FirstOrDefault());
            diagnosticContext.Set("UserName", httpContext.User.Identity?.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}