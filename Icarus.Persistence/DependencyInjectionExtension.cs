using Icarus.Application.Abstractions.Data;
using Icarus.Domain.Repositories;
using Icarus.Persistence.Interceptors;
using Icarus.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Icarus.Persistence;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.ConfigureOptions<DbOptionsSetup>();

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

        services.AddScoped<IDbContext>(implementation => implementation.GetRequiredService<IcarusDbContext>());

        services.AddScoped<IUnitOfWork>(implementation => implementation.GetRequiredService<IcarusDbContext>());

        services.AddScoped<IMemberRepository, MemberRepository>();

        services.Decorate<IMemberRepository, CacheMemberRepository>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        return services;
    }
}
