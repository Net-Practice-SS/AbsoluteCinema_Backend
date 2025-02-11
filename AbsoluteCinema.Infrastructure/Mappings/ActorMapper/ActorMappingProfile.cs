using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.ActorsDTO;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.ActorMapper
{
    public class ActorMappingProfile : Profile
    {
        public ActorMappingProfile()
        {
            CreateMap<UpdateActorDto, ActorDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateActorDto, ActorDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
