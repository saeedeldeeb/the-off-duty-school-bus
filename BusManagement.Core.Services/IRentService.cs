using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Core.Services;

public interface IRentService
{
    public Task<RentVM> CreateRentAsync(RentDTO rent);
    public Task<RentVM> UpdateRentAsync(RentDTO rent, Guid id);
}
