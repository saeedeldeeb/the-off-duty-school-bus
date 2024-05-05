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

public class OffDutiesService : IOffDutiesService
{
    private readonly IOffDutyRepository _offDutyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OffDutiesService(IOffDutyRepository offDutyRepository, IUnitOfWork unitOfWork)
    {
        _offDutyRepository = offDutyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<OffDutyVM>> GetAll(IOffDutyResourceParameters parameters)
    {
        var pagedOffDuties = await _offDutyRepository.GetAllWithParametersAsync(parameters);
        var offDuties = pagedOffDuties.Parse<IEnumerable<OffDuty>, IEnumerable<OffDutyVM>>();
        return PagedList<OffDutyVM>.CreateAsync(
            offDuties,
            pagedOffDuties.TotalCount,
            pagedOffDuties.CurrentPage,
            pagedOffDuties.PageSize
        );
    }

    public async Task<OffDutyVM> GetById(Guid id)
    {
        var offDuty = await _offDutyRepository.FindAsync(x => x.Id == id, ["Vehicle"]);
        if (offDuty == null)
        {
            throw new Exception("OffDuty not found");
        }
        return offDuty.Parse<OffDuty, OffDutyVM>();
    }

    public OffDutyVM Add(OffDutyDTO offDuty)
    {
        var offDutyEntity = offDuty.Parse<OffDutyDTO, OffDuty>();
        var addedOffDuty = _offDutyRepository.Add(offDutyEntity);
        _unitOfWork.Complete();

        return addedOffDuty.Parse<OffDuty, OffDutyVM>();
    }

    public OffDutyVM Update(OffDutyDTO offDuty, Guid id)
    {
        var offDutyEntity = offDuty.Parse<OffDutyDTO, OffDuty>();
        var updatedOffDuty = _offDutyRepository.Update(offDutyEntity, id);
        _unitOfWork.Complete();

        return updatedOffDuty.Parse<OffDuty, OffDutyVM>();
    }

    public void Delete(Guid id)
    {
        var offDuty = _offDutyRepository.GetById(id);
        if (offDuty == null)
        {
            throw new Exception("OffDuty not found");
        }

        _offDutyRepository.Delete(offDuty);
        _unitOfWork.Complete();
    }
}
