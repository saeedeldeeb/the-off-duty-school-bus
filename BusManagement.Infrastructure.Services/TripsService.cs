using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;

namespace BusManagement.Infrastructure.Services;

public class TripsService : ITripsService
{
    private readonly ITripRepository _tripRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TripsService(ITripRepository tripRepository, IUnitOfWork unitOfWork)
    {
        _tripRepository = tripRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<TripVM>> GetAll(ITripResourceParameters parameters)
    {
        var pagedTrips = await _tripRepository.GetAllWithParametersAsync(parameters);
        var trips = pagedTrips.Parse<IEnumerable<Trip>, IEnumerable<TripVM>>();
        return PagedList<TripVM>.CreateAsync(
            trips,
            pagedTrips.TotalCount,
            pagedTrips.CurrentPage,
            pagedTrips.PageSize
        );
    }

    public async Task<TripVM> GetById(Guid id)
    {
        var trip = await _tripRepository.FindAsync(x => x.Id == id, ["Vehicle"]);
        if (trip == null)
        {
            throw new Exception("Trip not found");
        }

        return trip.Parse<Trip, TripVM>();
    }

    public TripVM Add(TripDTO trip)
    {
        var tripEntity = trip.Parse<TripDTO, Trip>();
        var addedTrip = _tripRepository.Add(tripEntity);
        _unitOfWork.Complete();

        return addedTrip.Parse<Trip, TripVM>();
    }

    public TripVM Update(TripDTO trip, Guid id)
    {
        var tripEntity = trip.Parse<TripDTO, Trip>();
        tripEntity.Id = id;
        var updatedTrip = _tripRepository.Update(tripEntity, id);
        _unitOfWork.Complete();

        return updatedTrip.Parse<Trip, TripVM>();
    }

    public void Delete(Guid id)
    {
        var trip = _tripRepository.GetById(id);
        if (trip == null)
        {
            throw new Exception("Trip not found");
        }

        _tripRepository.Delete(trip);
        _unitOfWork.Complete();
    }
}
