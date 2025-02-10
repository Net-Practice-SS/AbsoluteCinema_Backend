using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.SessionMapper;

public class SessionMappingProfile : Profile
{
    public SessionMappingProfile()
    {
        CreateMap<Session, SessionDto>().ReverseMap();
        
        CreateMap<CreateSessionDto, Session>();
        
        CreateMap<UpdateSessionDto, Session>();
    }
}