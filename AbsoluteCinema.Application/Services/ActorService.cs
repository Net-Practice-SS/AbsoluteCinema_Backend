using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.ActorsDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Strategies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AbsoluteCinema.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateActorAsync(CreateActorDto createActorDto)
        {
            var actorDto = _mapper.Map<ActorDto>(createActorDto);
            var actor = _mapper.Map<Actor>(actorDto);
            _unitOfWork.Repository<Actor>().Add(actor);
            await _unitOfWork.SaveChangesAsync();
            return actor.Id;
        }

        public async Task DeleteActorAsync(int id)
        {
            _unitOfWork.Repository<Actor>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActorDto>> GetAllActorsAsync(GetAllActorDto getAllActorDto)
        {
            Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy =
                query => query.OrderBy($"{getAllActorDto.OrderByProperty} {getAllActorDto.OrderDirection}");

            var actors = await _unitOfWork.Repository<Actor>().GetAllAsync(orderBy);
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }

        public async Task<ActorDto?> GetActorByIdAsync(int id)
        {
            var actor = await _unitOfWork.Repository<Actor>().GetByIdAsync(id);

            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", id.ToString());

            return _mapper.Map<ActorDto>(actor);
        }

        public async Task<IEnumerable<ActorDto>> GetActorWithStrategyAsync(
            GetActorWithStrategyDto getActorWithStrategyDto)
        {
            var strategy = new ActorStrategy(
                getActorWithStrategyDto.FirstName!,
                getActorWithStrategyDto.LastName!);

            Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy =
                query => query.OrderBy(
                    $"{getActorWithStrategyDto.OrderByProperty} {getActorWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.Repository<Actor>().GetWithStrategy(strategy, orderBy);
            var genres = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ActorDto>>(genres);
        }

        public async Task UpdateActorAsync(UpdateActorDto updateActorDto)
        {
            var currentActorDto = await GetActorByIdAsync(updateActorDto.Id);

            if (currentActorDto == null)
            {
                throw new EntityNotFoundException(nameof(Actor), "Id", updateActorDto.Id.ToString());
            }

            _mapper.Map(updateActorDto, currentActorDto);

            var actor = _mapper.Map<Actor>(currentActorDto);
            _unitOfWork.Repository<Actor>().Update(actor);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
