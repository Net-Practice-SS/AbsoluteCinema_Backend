using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.TicketsDTO;
using AbsoluteCinema.Domain.Entities;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.TicketMapping
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            // Маппинг для преобразования между Ticket и TicketDto
            CreateMap<Ticket, TicketDto>().ReverseMap();

            // Добавляем маппинг для преобразования CreateTicketDto -> Ticket
            CreateMap<CreateTicketDto, Ticket>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Аналогично можно добавить маппинг для обновления, если требуется:
            CreateMap<UpdateTicketDto, Ticket>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}