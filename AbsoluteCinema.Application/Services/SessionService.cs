﻿using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AutoMapper;
using System.Linq.Dynamic.Core;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Strategies;
using Microsoft.EntityFrameworkCore;
using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;


namespace AbsoluteCinema.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateSessionAsync(CreateSessionDto createSessionDto)
        {
            var session = _mapper.Map<Session>(createSessionDto);
            _unitOfWork.Repository<Session>().Add(session);
            await _unitOfWork.SaveChangesAsync();
            return session.Id;
        }

        public async Task DeleteSessionAsync(int id)
        {
            _unitOfWork.Repository<Session>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsAsync(GetAllSessionDto getDto)
        {
            Func<IQueryable<Session>, IOrderedQueryable<Session>> orderBy =
                query => query.OrderBy($"{getDto.OrderByProperty} {getDto.OrderDirection}");

            var sessions = await _unitOfWork.Repository<Session>().GetAllAsync(orderBy);
            return _mapper.Map<IEnumerable<SessionDto>>(sessions);
        }


        public async Task<SessionDto?> GetSessionByIdAsync(int id)
        {
            var session = await _unitOfWork.Repository<Session>().GetByIdAsync(id);
            return _mapper.Map<SessionDto>(session);
        }

        public async Task UpdateSessionAsync(UpdateSessionDto updateSessionDto)
        {
            var currentSessionDto = await GetSessionByIdAsync(updateSessionDto.Id);

            if (currentSessionDto == null)
            {
                throw new EntityNotFoundException(nameof(Session), "Id", updateSessionDto.Id.ToString());
            }

            _mapper.Map(updateSessionDto, currentSessionDto);

            var session = _mapper.Map<Session>(currentSessionDto);
            _unitOfWork.Repository<Session>().Update(session);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SessionDto>> GetSessionWithStrategyAsync(GetSessionWithStrategyDto getSessionWithStrategyDto)
        {
            var strategy = new SessionStrategy(getSessionWithStrategyDto.MovieId, getSessionWithStrategyDto.Date, getSessionWithStrategyDto.HallId);
            Func<IQueryable<Session>, IOrderedQueryable<Session>> orderBy =
                query => query.OrderBy($"{getSessionWithStrategyDto.OrderByProperty} {getSessionWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.Repository<Session>().GetWithStrategy(strategy, orderBy);

            var sessions = await query.ToListAsync();
            return _mapper.Map<IEnumerable<SessionDto>>(sessions);
        }

    }
}
