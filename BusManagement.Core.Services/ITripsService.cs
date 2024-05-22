using BusManagement.Core.Common.Helpers;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories.ResourceParameters;

namespace BusManagement.Core.Services;

public interface ITripsService
{
    Task<PagedList<TripVM>> GetAll(ITripResourceParameters parameters);
    Task<TripVM> GetById(Guid id);
    TripVM Add(TripDTO trip);
    TripVM Update(TripDTO trip, Guid id);
    void Delete(Guid id);
}
