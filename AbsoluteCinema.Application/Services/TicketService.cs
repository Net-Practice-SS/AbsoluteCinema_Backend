using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.TicketsDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Strategies;
using AbsoluteCinema.Domain.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using AbsoluteCinema.Application.DTO.Entities;

namespace AbsoluteCinema.Application.Services
{
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
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(updateTicketDto.Id);
            if (ticket == null)
                throw new EntityNotFoundException(nameof(Ticket), "Id", updateTicketDto.Id.ToString());
            
            _mapper.Map(updateTicketDto, ticket);
            _unitOfWork.Repository<Ticket>().Update(ticket);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteTicketAsync(int ticketId)
        {
            _unitOfWork.Repository<Ticket>().Delete(ticketId);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync(GetAllTicketsDto getAllTicketsDto)
        {
            Func<IQueryable<Ticket>, IOrderedQueryable<Ticket>> orderBy =
                query => query.OrderBy($"{getAllTicketsDto.OrderByProperty} {getAllTicketsDto.OrderDirection}");
            
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync(orderBy, include: null, page: getAllTicketsDto.Page, getAllTicketsDto.PageSize);
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
        
        public async Task<TicketDto?> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(id);
            return ticket == null ? null : _mapper.Map<TicketDto>(ticket);
        }
        
        public async Task<IEnumerable<TicketDto>> GetTicketWithStrategyAsync(GetTicketWithStrategyDto getTicketWithStrategyDto)
        {
            var strategy = new TicketStrategy(
                sessionId: getTicketWithStrategyDto.SessionId,
                userId: getTicketWithStrategyDto.UserId,
                row: getTicketWithStrategyDto.Row,
                place: getTicketWithStrategyDto.Place,
                statusId: getTicketWithStrategyDto.StatusId,
                price: getTicketWithStrategyDto.Price
            );
            
            Func<IQueryable<Ticket>, IOrderedQueryable<Ticket>> orderBy =
                query => query.OrderBy($"{getTicketWithStrategyDto.OrderByProperty} {getTicketWithStrategyDto.OrderDirection}");
            
            var query = _unitOfWork.Repository<Ticket>().GetWithStrategy(strategy, orderBy, page: getTicketWithStrategyDto.Page, getTicketWithStrategyDto.PageSize);
            var tickets = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task UpdateTicketStatusAsync(int ticketId, TicketStatusIdDto ticketStatusIdDto)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(ticketId);
            if (ticket == null)
                throw new EntityNotFoundException(nameof(Ticket), "Id", ticketId.ToString());
            
            if (ticketStatusIdDto == null)
                throw new EntityNotFoundException(nameof(TicketStatusDto), "Id", ticketStatusIdDto.Id.ToString());
            
            ticket.StatusId = ticketStatusIdDto.Id;
            
            _unitOfWork.Repository<Ticket>().Update(ticket);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<TicketDto>> GetAllTicketsWithIncludeAsync(GetAllTicketsDto getAllTicketsDto)
        {
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync(
                orderBy: query => query.OrderBy(t => t.Id),
                include: query => query
                    .Include(t => t.Session)
                        .ThenInclude(s => s.Movie)
                    .Include(t => t.Session)
                        .ThenInclude(s => s.Hall)
                    .Include(t => t.ApplicationUser)
                    .Include(t => t.Status),
                page: getAllTicketsDto.Page, getAllTicketsDto.PageSize
                    
            );
            
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);;
        }
        
        public async Task<IEnumerable<TicketDto>> GetTicketsForUserAsync(int userId)
        {
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync(
                include: query => query
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Movie)
                    .Include(t => t.Session)
                    .ThenInclude(s => s.Hall)
                    .Include(t => t.ApplicationUser)
                    .Include(t => t.Status),
                    orderBy: query => query.OrderBy(t => t.Id)
            );
            
            tickets = tickets.Where(t => t.UserId == userId);
            
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);;
        }
    }
}
