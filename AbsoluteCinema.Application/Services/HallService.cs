using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.HallsDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Strategies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AbsoluteCinema.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HallService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateHallAsync(CreateHallDto createHallDto)
        {
            var hallDto = _mapper.Map<HallDto>(createHallDto);
            var hall = _mapper.Map<Hall>(hallDto);
            _unitOfWork.Repository<Hall>().Add(hall);
            await _unitOfWork.SaveChangesAsync();
            return hall.Id;
        }

        public async Task DeleteHallAsync(int id)
        {
            _unitOfWork.Repository<Hall>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<HallDto>> GetAllHallsAsync(GetAllHallDto getAllHallDto)
        {
            Func<IQueryable<Hall>, IOrderedQueryable<Hall>> orderBy =
                query => query.OrderBy($"{getAllHallDto.OrderByProperty} {getAllHallDto.OrderDirection}");

            var halls = await _unitOfWork.Repository<Hall>().GetAllAsync(orderBy, include: null, page: getAllHallDto.Page, getAllHallDto.PageSize);
            return _mapper.Map<IEnumerable<HallDto>>(halls);
        }

        public async Task<HallDto?> GetHallByIdAsync(int id)
        {
            var hall = await _unitOfWork.Repository<Hall>().GetByIdAsync(id);

            if (hall == null)
                throw new EntityNotFoundException(nameof(Hall), "Id", id.ToString());

            return _mapper.Map<HallDto>(hall);
        }

        public async Task<IEnumerable<HallDto>> GetHallWithStrategyAsync(GetHallWithStrategyDto getHallWithStrategyDto)
        {
            var strategy = new HallStrategy(
                getHallWithStrategyDto.Name,
                getHallWithStrategyDto.RowCount,
                getHallWithStrategyDto.PlaceCount);

            Func<IQueryable<Hall>, IOrderedQueryable<Hall>> orderBy =
                query => query.OrderBy($"{getHallWithStrategyDto.OrderByProperty} {getHallWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.Repository<Hall>().GetWithStrategy(strategy, orderBy, page: getHallWithStrategyDto.Page, getHallWithStrategyDto.PageSize);
            var halls = await query.ToListAsync();
            return _mapper.Map<IEnumerable<HallDto>>(halls);
        }

        public async Task UpdateHallAsync(UpdateHallDto updateHallDto)
        {
            var currentHallDto = await GetHallByIdAsync(updateHallDto.Id);

            if (currentHallDto == null)
            {
                throw new EntityNotFoundException(nameof(Hall), "Id", updateHallDto.Id.ToString());
            }

            _mapper.Map(updateHallDto, currentHallDto);

            var hall = _mapper.Map<Hall>(currentHallDto);
            _unitOfWork.Repository<Hall>().Update(hall);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
