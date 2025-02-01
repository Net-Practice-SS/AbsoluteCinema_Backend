using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Application.Mappings.EntityMapper;

public class EntityMappingProfile : Profile
{
    public EntityMappingProfile()
    {
        // Actor
        CreateMap<Actor, ActorDto>().ReverseMap();

        // Genre
        CreateMap<Genre, GenreDto>().ReverseMap();

        // Hall
        CreateMap<Hall, HallDto>().ReverseMap();

        // Movie
        CreateMap<Movie, MovieDto>().ReverseMap();

        // Session
        CreateMap<Session, SessionDto>().ReverseMap();

        // Ticket
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.PlacementId, opt => opt.Ignore())
            .ReverseMap();

        // TicketStatus
        CreateMap<TicketStatus, TicketStatusDto>().ReverseMap();
    }
}