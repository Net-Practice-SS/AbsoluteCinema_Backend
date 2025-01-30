using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Application.Mappings.EntityMapper;

public class EntityMapperProfile : Profile
{
    public EntityMapperProfile()
    {
        // Actor
        CreateMap<Actor, ActorDto>().ReverseMap();

        // Genre
        CreateMap<Genre, GenreDto>().ReverseMap();

        // Hall
        CreateMap<Hall, HallDto>().ReverseMap();

        // Movie
        CreateMap<Movie, MovieDto>().ReverseMap();

        // MovieActor
        CreateMap<MovieActor, MovieActorDto>().ReverseMap();

        // MovieGenre
        CreateMap<MovieGenre, MovieGenreDto>().ReverseMap();

        // Session
        CreateMap<Session, SessionDto>().ReverseMap();

        // Ticket
        CreateMap<Ticket, TicketDto>().ReverseMap();

        // TicketStatus
        CreateMap<TicketStatus, TicketStatusDto>().ReverseMap();
    }
}