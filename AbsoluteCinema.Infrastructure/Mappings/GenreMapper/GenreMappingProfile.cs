using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.GenresDTO;
using AbsoluteCinema.Application.DTO.HallsDTO;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.GenreMapper
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<UpdateGenreDto, GenreDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateGenreDto, GenreDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
