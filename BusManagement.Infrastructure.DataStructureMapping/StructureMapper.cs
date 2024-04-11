using AutoMapper;
using BusManagement.Core.Data;
using BusManagement.Core.DataModel.DTOs;

namespace BusManagement.Infrastructure.DataStructureMapping;

public class StructureMapper
{
    public static IMapper Mapper { get; private set; }

    public static void Initialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BrandDTO, VehicleBrand>()
                .ForMember(
                    dest => dest.Translations.Select(x => x.Name),
                    opt => opt.MapFrom(src => src.Translations.Select(x => x.Name))
                )
                .ForMember(
                    dest => dest.Translations.Select(x => x.Language),
                    opt => opt.MapFrom(src => src.Translations.Select(x => x.Language))
                );
        });

        Mapper = config.CreateMapper();
    }
}
