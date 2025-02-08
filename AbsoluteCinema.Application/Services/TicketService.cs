using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.TicketsDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AutoMapper;

namespace AbsoluteCinema.Application.Services;

public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateTicketAsync(CreateTicketDto createTicketDto)
    {
        var ticket = _mapper.Map<Ticket>(createTicketDto);
        _unitOfWork.Repository<Ticket>().Add(ticket);
        
        await _unitOfWork.SaveChangesAsync();
        return ticket.Id;
    }

    public async Task UpdateTicketAsync(UpdateTicketDto updateTicketDto)
    {
        var ticket = _mapper.Map<Ticket>(updateTicketDto);
        _unitOfWork.Repository<Ticket>().Update(ticket);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(int ticketId)
    {
        _unitOfWork.Repository<Ticket>().Delete(ticketId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync();
        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }
    
    public async Task<TicketDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);
        return ticket == null ? null : _mapper.Map<TicketDto>(ticket);
    } 
}