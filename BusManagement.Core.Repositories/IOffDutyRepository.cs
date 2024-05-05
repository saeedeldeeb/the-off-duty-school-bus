using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Repositories;

public interface IOffDutyRepository : IBaseRepository<OffDuty>
{
    public Task<PagedList<OffDuty>> GetAllWithParametersAsync(
        IOffDutyResourceParameters parameters
    );
}
