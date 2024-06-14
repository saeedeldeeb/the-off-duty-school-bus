using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Common.MessageBroker;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Repositories.ResourceParameters;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;
using BusManagement.Infrastructure.RabbitMQ.Events;

namespace BusManagement.Infrastructure.Services;

public class TripsService : ITripsService
{
    private readonly IRabbitMqProducer<TripStarted> _tripProducer;
    private readonly ITripRepository _tripRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TripsService(
        ITripRepository tripRepository,
        IUnitOfWork unitOfWork,
        IRabbitMqProducer<TripStarted> tripProducer
    )
    {
        _tripRepository = tripRepository;
        _unitOfWork = unitOfWork;
        _tripProducer = tripProducer;
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

    public void StartTrip(Guid id)
    {
        var trip = _tripRepository.GetById(id);
        if (trip == null)
        {
            throw new Exception("Trip not found");
        }

        var tripStarted = new TripStarted
        {
            TripId = trip.Id,
            BusId = trip.VehicleId ?? Guid.Empty,
            StartTime = DateTime.Now,
            StartLocation = trip.StartPoint.ToString(),
            EndLocation = trip.EndPoint.ToString()
        };
        _tripProducer.Publish(tripStarted);
    }
}
