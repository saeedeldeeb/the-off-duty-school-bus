using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Core.Repositories;
using BusManagement.Core.Repositories.Base;
using BusManagement.Core.Services;
using BusManagement.Infrastructure.DataStructureMapping;

namespace BusManagement.Infrastructure.Services;

public class RentService : IRentService
{
    private readonly IRentRepository _rentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RentService(IRentRepository rentRepository, IUnitOfWork unitOfWork)
    {
        _rentRepository = rentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RentVM> CreateRentAsync(RentDTO rent)
    {
        var rentEntity = rent.Parse<RentDTO, Rent>();
        var addedRent = await _rentRepository.AddAsync(rentEntity);
        await _unitOfWork.CompleteAsync();

        return addedRent.Parse<Rent, RentVM>();
    }

    public async Task<RentVM> UpdateRentAsync(RentDTO rent, Guid id)
    {
        var rentEntity = rent.Parse<RentDTO, Rent>();
        rentEntity.Id = id;
        var updatedRent = _rentRepository.Update(rentEntity, rentEntity.Id);
        await _unitOfWork.CompleteAsync();

        return updatedRent.Parse<Rent, RentVM>();
    }
}
