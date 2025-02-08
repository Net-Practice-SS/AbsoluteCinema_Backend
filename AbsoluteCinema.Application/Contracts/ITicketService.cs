using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.TicketsDTO;

namespace AbsoluteCinema.Application.Contracts;

public interface ITicketService
{
    Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
    Task<TicketDto?> GetTicketByIdAsync(int id);
    Task<int> CreateTicketAsync(CreateTicketDto createTicketDto);
    Task UpdateTicketAsync(UpdateTicketDto updateTicketDto);
    Task DeleteTicketAsync(int id);
}