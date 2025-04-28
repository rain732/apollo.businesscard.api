using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Apollo.BusinessCard.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );
        services.AddHttpContextAccessor();
        services.AddScoped<Apollo.BusinessCard.Application.Common.DTOs.LoggedUser>();
        services.AddScoped<IBase64Service, Base64Service>();
        return services;
    }
}
