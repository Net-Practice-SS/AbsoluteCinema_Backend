using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.TicketsDTO;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.TicketMapper;
public class TicketMappingProfile : Profile 
{ 
    public TicketMappingProfile()
    {
        CreateMap<Ticket, TicketDto>().ReverseMap();
            
        CreateMap<CreateTicketDto, Ticket>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
        CreateMap<UpdateTicketDto, Ticket>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
