using Icarus.App.Configuration;
using Icarus.App.Middlewares;
using Icarus.Application;
using Icarus.Infrastructure;
using Icarus.Persistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging()
    .AddApplication()
    .AddPersistence()
    .AddInfrastructureBackgroundJob()
    .AddInfrastructureAuthentication(builder.Configuration)
    .AddInfrastructureServices()
    .AddPresentation()
    .AddIfNotSeen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (ctx, next) =>
{
    Console.WriteLine("Before HttpRequest");
    await next(ctx);
    Console.WriteLine("After HttpRequest");
});

app.UseCors("AllowAllOrigins");

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
