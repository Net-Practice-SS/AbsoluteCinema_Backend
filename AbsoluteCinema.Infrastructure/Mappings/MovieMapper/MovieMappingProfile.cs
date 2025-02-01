using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.MovieMapper
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<UpdateMovieDto, MovieDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
