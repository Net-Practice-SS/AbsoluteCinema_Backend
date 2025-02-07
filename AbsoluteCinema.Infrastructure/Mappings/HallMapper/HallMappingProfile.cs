using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.HallsDTO;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.HallMapper
{
    public class HallMappingProfile : Profile
    {
        public HallMappingProfile()
        {
            CreateMap<UpdateHallDto, HallDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateHallDto, HallDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
