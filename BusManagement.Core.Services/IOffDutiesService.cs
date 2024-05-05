using BusManagement.Core.Common.Helpers;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Services;

public interface IOffDutiesService
{
    Task<PagedList<OffDutyVM>> GetAll(IOffDutyResourceParameters parameters);
    Task<OffDutyVM> GetById(Guid id);
    OffDutyVM Add(OffDutyDTO offDuty);
    OffDutyVM Update(OffDutyDTO offDuty, Guid id);
    void Delete(Guid id);
}
