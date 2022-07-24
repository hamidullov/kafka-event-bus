using Example.Logging.Serilog.Extensions;
using Microsoft.Extensions.Logging;

namespace Example.Logging.Serilog;

public class SerilogHandler : DelegatingHandler
{
    private readonly ILogger<SerilogHandler> _logger;

    public SerilogHandler(ILogger<SerilogHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var requestBody = string.Empty;
        if (request.Content != null)
        {
            requestBody = await request.Content.ReadAsStringAsync(cancellationToken);
            if (request.Content.Headers.ContentType?.MediaType ==  "application/xml")
            {
                requestBody = requestBody.SanitizingXmlLog();
            }
            else if (request.Content.Headers.ContentType?.MediaType == "application/json")
            {
                requestBody = requestBody.SanitizingLog();
            }
        }

        _logger.LogInformation("Request starting: {route} {method} {headers}\n{body}", request.RequestUri.AbsoluteUri,
            request.Method, request.Headers, requestBody);
        var response = await base.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogInformation("Request completed: {route} {method} {code} {headers}\n{body}",
            request.RequestUri.AbsoluteUri, request.Method, response.StatusCode, request.Headers, responseBody);
        return response;
    }
}