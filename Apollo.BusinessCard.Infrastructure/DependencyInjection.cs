using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Infrastructure.Persistiance;
using Apollo.BusinessCard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.BusinessCard.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration _configuration)
    {
        AddDbContext(services, _configuration);
        AddServices(services, _configuration);
        
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")
                );
        });

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>()
        );
    }

    private static void AddServices(this IServiceCollection services, IConfiguration _configuration)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();
    }
}
