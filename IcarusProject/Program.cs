using Icarus.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Icarus.App.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<DbOptionsSetup>();

builder.Services.AddDbContext<IcarusDbContext>(
    (provider, options) =>
{
    var dbOptions = provider.GetService<IOptions<DatabaseOptions>>()!.Value;

    options.UseSqlServer(dbOptions.ConnectionString,
        action =>
        {
            action.EnableRetryOnFailure(dbOptions.MaxRetryCount);
            action.CommandTimeout(dbOptions.CommandTimeout);
        });
    options.EnableDetailedErrors(dbOptions.EnableDetailError);
    options.EnableSensitiveDataLogging(dbOptions.EnableSensitiveLogging);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
