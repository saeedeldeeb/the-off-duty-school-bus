using AutoMapper;
using BusManagement.Core.Data;
using BusManagement.Core.Data.MultiLingualObjects;
using BusManagement.Core.DataModel.DTOs;
using BusManagement.Core.DataModel.ViewModels;
using BusManagement.Infrastructure.DataStructureMapping.Converters;
using NetTopologySuite.Geometries;

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

            cfg.CreateMap<TripDTO, Trip>()
                .ForMember(
                    dest => dest.StartPoint,
                    opt => opt.ConvertUsing(new StringToPointConverter(), src => src.StartPoint)
                )
                .ForMember(
                    dest => dest.EndPoint,
                    opt => opt.ConvertUsing(new StringToPointConverter(), src => src.EndPoint)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.StartPoint,
                    opt => opt.MapFrom(src => $"{src.StartPoint.Y},{src.StartPoint.X}") // Convert back to string in 'lat,lng' format for the DTO.
                )
                .ForMember(
                    dest => dest.EndPoint,
                    opt => opt.MapFrom(src => $"{src.EndPoint.Y},{src.EndPoint.X}") // Convert back to string in 'lat,lng' format for the DTO.
                );

            cfg.CreateMap<Trip, TripVM>()
                .ForMember(
                    dest => dest.StartPoint,
                    opt => opt.MapFrom(src => $"{src.StartPoint.Y},{src.StartPoint.X}") // Convert to string in 'lat,lng' format for the ViewModel.
                )
                .ForMember(
                    dest => dest.EndPoint,
                    opt => opt.MapFrom(src => $"{src.EndPoint.Y},{src.EndPoint.X}") // Convert to string in 'lat,lng' format for the ViewModel.
                );
        });

        _mapper = config.CreateMapper();
    }

    public static TO Parse<TFrom, TO>(this TFrom from)
    {
        return _mapper.Map<TFrom, TO>(from);
    }
}
