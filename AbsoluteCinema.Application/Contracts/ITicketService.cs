using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.TicketsDTO;

namespace AbsoluteCinema.Application.Contracts;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllTicketsAsync(GetAllTicketsDto getAllTicketsDto);
    Task<TicketDto?> GetTicketByIdAsync(int ticketId);
    Task DeleteTicketAsync(int ticketId);
    Task UpdateTicketAsync(UpdateTicketDto updateTicketDto);
    Task<int> CreateTicketAsync(CreateTicketDto createTicketDto);
    Task<IEnumerable<TicketDto>> GetTicketWithStrategyAsync(GetTicketWithStrategyDto getTicketWithStrategyDto);
    Task UpdateTicketStatusAsync(int ticketId, TicketStatusIdDto ticketStatusIdDto);
    Task<IEnumerable<TicketDto>> GetAllTicketsWithIncludeAsync(GetAllTicketsDto getAllTicketsDto);
    Task<IEnumerable<TicketDto>> GetTicketsForUserAsync(int userId);
}