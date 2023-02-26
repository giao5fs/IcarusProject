using Icarus.Application.Abstractions.Authentication;
using Icarus.Application.Abstractions.Caching;
using Icarus.Application.Abstractions.Cryptography;
using Icarus.Application.Members.Events;
using Icarus.Domain.DomainEvents;
using Icarus.Domain.Services;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Infrastructure.Authentication.Constants;
using Icarus.Infrastructure.Authentication.Cryptography;
using Icarus.Infrastructure.Authentication.Permissions;
using Icarus.Infrastructure.Authentication.Providers;
using Icarus.Infrastructure.BackgroundJobs;
using Icarus.Infrastructure.Idempotence;
using Icarus.Infrastructure.Options;
using Icarus.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Reflection;
using System.Text;

namespace Icarus.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        //services.AddMediatR(typeof(MemberRegisteredDomainEventHandler));
        //services.AddMediatR(typeof(PerformBackgroundCheckWhenMemberRegisteredDomainEventHandler));

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureAuthentication(this IServiceCollection services)
    {

        services.ConfigureOptions<JwtOptionsSetup>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtDefault.AuthenticationScheme).AddJwtBearer();

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddScoped<IClaimsProvider, ClaimProvider>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddHttpContextAccessor();

        services.AddScoped<IMemberIdentifierProvider, MemberIdentifierProvider>();

        services.AddSingleton<IPermissionService, PermissionService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureBackgroundJob(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                trigger =>
                    trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(30)
                    .RepeatForever()));
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}
