using System.Net;
using Example.Logging.Serilog.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Example.Logging.Serilog.Extensions;

public static class LoggingSerilogExtensions
{
    public static void AddLoggingSerilogServices(this IServiceCollection services)
    {
        services.AddTransient<SerilogHandler>();
        services.AddTransient<RequestResponseLoggingMiddleware>();
        services.AddTransient<LoggingEnrichMiddleware>();
    }
        
    //UseLoggingSerilogRequestResponseBody

    public static void UseLoggingSerilogEnricher(this IApplicationBuilder builder, string kubeProxyIpAddress)
    {
        builder.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            KnownProxies = { IPAddress.Parse(kubeProxyIpAddress) }
        });
        builder.UseMiddleware<LoggingEnrichMiddleware>();    
        builder.UseSerilogRequestLogging();
    }

    public static void UseLoggingSerilogRequestResponseBody(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
        
    public static IHostBuilder UseLoggingSerilog(
        this IHostBuilder builder, 
        string project, 
        string applicationName, 
        string elasticUrl)
    {
        return builder.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl) ){
                AutoRegisterTemplate = true,
                IndexFormat = "csharp-{0:yyyy.MM}",
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                DeadLetterIndexName = "csharp-deadletter-dev-{0:yyyy.MM}",
                NumberOfReplicas = 0,
                RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage:true),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
                BatchAction = ElasticOpType.Create,
                TypeName = null,
                BufferBaseFilename = "./logs/docker-elk-serilog-web-buffer",
            })
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.WithProperty("ApplicationName", applicationName)
            .Enrich.WithProperty("Project", project)
        );
    }
}