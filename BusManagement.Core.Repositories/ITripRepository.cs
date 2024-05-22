using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Repositories;

public interface ITripRepository : IBaseRepository<Trip>
{
    public Task<PagedList<Trip>> GetAllWithParametersAsync(ITripResourceParameters parameters);
}
