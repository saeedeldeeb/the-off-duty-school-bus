using BusManagement.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusManagement.Infrastructure.Services;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IVehicleBrandService, VehicleBrandService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IOffDutiesService, OffDutiesService>();
        services.AddScoped<ITripsService, TripsService>();
        services.AddScoped<IProfileService, ProfileService>();
        return services;
    }
}
