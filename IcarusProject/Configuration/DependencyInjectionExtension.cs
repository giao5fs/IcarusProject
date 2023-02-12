using Icarus.App.Options;
using Icarus.Application.Abstractions;
using Icarus.Application.Behaviors;
using Icarus.Domain.Repositories;
using Icarus.Infrastructure.BackgroundJobs;
using Icarus.Persistence;
using Icarus.Persistence.Interceptors;
using Icarus.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Quartz;
using Scrutor;
using FluentValidation;

namespace Icarus.App.Configuration;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.Scan(
            selector => selector
            .FromAssemblies(
                Icarus.Infrastructure.AssemblyReference.Assembly,
                Icarus.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());


        //services.AddScoped<MemberRepository>();
        //services.AddScoped<IMemberRepository, CacheMemberRepository>();

        //services.AddScoped<IMemberRepository>(sp =>
        //{
        //    var context = sp.GetService<IcarusDbContext>();
        //    var cache = sp.GetService<IMemoryCache>();

        //    return new CacheMemberRepository(new MemberRepository(context!), cache!);
        //});

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
                    .WithIntervalInSeconds(120)
                    .RepeatForever()));
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.Decorate<IMemberRepository, CacheMemberRepository>();

        services.AddMemoryCache();

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

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Icarus.Application.AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(Icarus.Presentation.AssemblyReference.Assembly);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static IServiceCollection AddConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<DbOptionsSetup>();
        services.ConfigureOptions<JwtOptionsSetup>();
        //builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        return services;
    }

}
