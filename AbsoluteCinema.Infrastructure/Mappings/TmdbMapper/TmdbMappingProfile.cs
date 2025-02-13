using AbsoluteCinema.Application.DTO.ActorsDTO;
using AbsoluteCinema.Application.DTO.GenresDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO;
using AbsoluteCinema.Infrastructure.Converters;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.TmdbMapper
{
    public class TmdbMappingProfile : Profile
    {
        public TmdbMappingProfile()
        {
            CreateMap<TmdbMovieDto, CreateMovieDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(dest => dest.Discription, opt => opt.MapFrom(dto => dto.Overview))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(dto => dto.VoteAverage))
                .ForMember(dest => dest.Adult, opt => opt.MapFrom(dto => dto.Adult))
                .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(dto => dto.FullPosterPath))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(dto => LanguageConverter.ConvertToLanguage(dto.OriginalLanguage)))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.ReleaseDate!, "yyyy-MM-dd", null)))
                .ForMember(dest => dest.TrailerPath, opt => opt.Ignore());

            CreateMap<TmdbGenreDto, CreateGenreDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(dto => dto.Name));

            CreateMap<TmdbCastDto, CreateActorDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(dto => FullNameConverter.GetFirstName(dto.Name)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(dto => FullNameConverter.GetLastName(dto.Name)));
        }
    }
}
