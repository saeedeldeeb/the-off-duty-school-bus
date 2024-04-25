using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Infrastructure.Repositories.Base;
using BusManagement.Infrastructure.Repositories.ResourceParameters;
using Microsoft.Extensions.DependencyInjection;

namespace BusManagement.Infrastructure.Repositories;

public static class InfrastructureRepositoryRegistration
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleResourceParameters, VehicleResourceParameters>();
        return services;
    }
}
