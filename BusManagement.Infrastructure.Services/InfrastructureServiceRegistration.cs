using BusManagement.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusManagement.Infrastructure.Services;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}