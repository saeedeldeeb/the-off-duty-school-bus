using AutoMapper;
using BusManagement.Core.Data;
using BusManagement.Core.Data.MultiLingualObjects;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;

namespace BusManagement.Infrastructure.DataStructureMapping;

public static class StructureMapper
{
    private static IMapper _mapper;

    public static void Initialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            var multiLangualManager = new MultiLingualObjectManager();
            cfg.CreateMap<VehicleBrand, BrandVM>()
                .ForMember(
                    dest => dest.Name,
                    opt =>
                        opt.MapFrom(src =>
                            multiLangualManager
                                .FindTranslationAsync<VehicleBrand, VehicleBrandTranslation>(
                                    src,
                                    null,
                                    true
                                )
                                .Result.Name
                        )
                );

            cfg.CreateMap<TranslationDTO, VehicleBrandTranslation>();

            cfg.CreateMap<BrandDTO, VehicleBrand>()
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations));

            cfg.CreateMap<VehicleDTO, Vehicle>().ReverseMap();
            cfg.CreateMap<Vehicle, VehicleVM>();

            cfg.CreateMap<OffDutyDTO, OffDuty>().ReverseMap();
            cfg.CreateMap<OffDuty, OffDutyVM>();
        });

        _mapper = config.CreateMapper();
    }

    public static TO Parse<TFrom, TO>(this TFrom from)
    {
        return _mapper.Map<TFrom, TO>(from);
    }
}
