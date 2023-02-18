namespace Icarus.App.Configuration;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddIfNotSeen(this IServiceCollection services)
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
        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(Icarus.Presentation.AssemblyReference.Assembly);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        return services;
    }
}
