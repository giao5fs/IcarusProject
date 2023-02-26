using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace Icarus.App.Middlewares;

public sealed class LogContextEnrichmentMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.Push(GetEnrichers(context)))
        {
            await next(context);
        }
    }

    private static ILogEventEnricher[] GetEnrichers(HttpContext context)
    {
        return new ILogEventEnricher[]
        {
            new PropertyEnricher("IPAddress", context.Connection.RemoteIpAddress),
            new PropertyEnricher("RequestHost", context.Request.Host),
            new PropertyEnricher("RequestPathBase", context.Request.PathBase),
            new PropertyEnricher("RequestQueryParams", context.Request.QueryString)
        };
    }
}
