using Icarus.App.Configuration;
using Icarus.App.Middlewares;
using Icarus.Application;
using Icarus.Infrastructure;
using Icarus.Persistence;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging()
    .AddTransient<LogContextEnrichmentMiddleware>()
    .AddApplication()
    .AddPersistence()
    .AddInfrastructureBackgroundJob()
    .AddInfrastructureAuthentication()
    .AddInfrastructureServices()
    .AddPresentation()
    .AddIfNotSeen();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LogContextEnrichmentMiddleware>();

app.UseCors("AllowAllOrigins");

app.UseSerilogRequestLogging();

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
