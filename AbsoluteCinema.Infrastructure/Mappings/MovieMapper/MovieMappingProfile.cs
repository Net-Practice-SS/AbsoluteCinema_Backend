using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.MovieMapper
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<UpdateMovieDto, MovieDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateMovieDto, MovieDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<MovieGenre, MovieGenreDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Title));
            CreateMap<MovieActor, MovieActorDto>()
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Actor.LastName));
            
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.MovieGenre))
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.MovieActor));
        }
    }
}
