using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Repositories;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    public Task<PagedList<Vehicle>> GetAllWithParametersAsync(
        IVehicleResourceParameters parameters
    );
}
