using Icarus.App.Options;
using Icarus.Application.Abstractions;
using Icarus.Persistence;
using Icarus.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Icarus.App.Configuration;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.Scan(
            selector => selector.FromAssemblies(
                Icarus.Infrastructure.AssemblyReference.Assembly,
                Icarus.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<IcarusDbContext>(
            (provider, options) =>
            {
                var dbOptions = provider.GetService<IOptions<DatabaseOptions>>()!.Value;

                var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();
                var outboxMessageInterceptor = provider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                options.UseSqlServer(dbOptions.ConnectionString,
                    action =>
                    {
                        action.EnableRetryOnFailure(dbOptions.MaxRetryCount);
                        action.CommandTimeout(dbOptions.CommandTimeout);
                    }).AddInterceptors(auditableInterceptor!, outboxMessageInterceptor!);

                options.EnableDetailedErrors(dbOptions.EnableDetailError);
                options.EnableSensitiveDataLogging(dbOptions.EnableSensitiveLogging);
            });
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Icarus.Application.AssemblyReference.Assembly);
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(Icarus.Presentation.AssemblyReference.Assembly);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }


}
