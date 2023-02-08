using Icarus.App.Options;
using Icarus.Persistence.Interceptors;
using Icarus.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Icarus.App.Configuration;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.Scan(
            selector => selector.FromAssemblies(
                Icarus.Infrastructure.AssemblyReference.Assembly,
                Icarus.Persistence.AssemblyReference.Assembly,
                Icarus.Domain.AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.ConfigureOptions<DbOptionsSetup>();
        services.AddDbContext<IcarusDbContext>(
            (provider, options) =>
            {
                var dbOptions = provider.GetService<IOptions<DatabaseOptions>>()!.Value;

                var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();

                options.UseSqlServer(dbOptions.ConnectionString,
                    action =>
                    {
                        action.EnableRetryOnFailure(dbOptions.MaxRetryCount);
                        action.CommandTimeout(dbOptions.CommandTimeout);
                    }).AddInterceptors(auditableInterceptor!);

                options.EnableDetailedErrors(dbOptions.EnableDetailError);
                options.EnableSensitiveDataLogging(dbOptions.EnableSensitiveLogging);
            });
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Icarus.Application.AssemblyReference.Assembly);
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
