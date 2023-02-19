using Icarus.Application.Abstractions.Authentication;
using Icarus.Application.Abstractions.Caching;
using Icarus.Application.Abstractions.Cryptography;
using Icarus.Domain.Services;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Infrastructure.Authentication.Constants;
using Icarus.Infrastructure.Authentication.Cryptography;
using Icarus.Infrastructure.Authentication.Permissions;
using Icarus.Infrastructure.Authentication.Providers;
using Icarus.Infrastructure.BackgroundJobs;
using Icarus.Infrastructure.Options;
using Icarus.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace Icarus.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureAuthentication(this IServiceCollection services, IConfiguration _configuration)
    {

        services.ConfigureOptions<JwtOptionsSetup>();

        //services.ConfigureOptions<JwtBearerOptionsSetup>();

        //services.AddAuthentication(JwtDefault.AuthenticationScheme).AddJwtBearer();

        services.AddAuthentication(JwtDefault.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration[JwtDefault.IssuerSettingsKey],
                ValidAudience = _configuration[JwtDefault.AudienceSettingsKey],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtDefault.SecretSettingsKey]!))
            });

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddScoped<IClaimsProvider, ClaimProvider>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddHttpContextAccessor();

        services.AddScoped<IMemberIdentifierProvider, MemberIdentifierProvider>();

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
