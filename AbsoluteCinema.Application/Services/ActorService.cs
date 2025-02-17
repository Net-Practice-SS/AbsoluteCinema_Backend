using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
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
        
        public async Task AddMovieToActorAsync(MovieActorDto movieActorDto)
        {
            var movieActor = _mapper.Map<MovieActor>(movieActorDto);
            _unitOfWork.ActorRepository.AddMovieToActor(movieActor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> CreateActorAsync(CreateActorDto createActorDto)
        {
            var actorDto = _mapper.Map<ActorDto>(createActorDto);
            var actor = _mapper.Map<Actor>(actorDto);
            _unitOfWork.ActorRepository.Add(actor);
            await _unitOfWork.SaveChangesAsync();
            return actor.Id;
        }

        public async Task DeleteActorAsync(int id)
        {
            _unitOfWork.ActorRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteMovieFromActorAsync(MovieActorDto movieActorDto)
        {
            var movieActor = _mapper.Map<MovieActor>(movieActorDto);
            _unitOfWork.ActorRepository.DeleteMovieFromActor(movieActor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActorDto>> GetAllActorsAsync(GetAllActorDto getAllActorDto)
        {
            Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy =
                query => query.OrderBy($"{getAllActorDto.OrderByProperty} {getAllActorDto.OrderDirection}");

            var actors = await _unitOfWork.ActorRepository.GetAllAsync(orderBy, include: null, page: getAllActorDto.Page, getAllActorDto.PageSize);
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }

        public async Task<ActorDto?> GetActorByIdAsync(int id)
        {
            var actor = await _unitOfWork.ActorRepository.GetByIdAsync(id);

            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", id.ToString());

            return _mapper.Map<ActorDto>(actor);
        }

        public async Task<IEnumerable<ActorDto>> GetActorWithStrategyAsync(
            GetActorWithStrategyDto getActorWithStrategyDto)
        {
            var strategy = new ActorStrategy(
                getActorWithStrategyDto.FirstName!,
                getActorWithStrategyDto.LastName!,
                getActorWithStrategyDto.MoviesIds);

            Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy =
                query => query.OrderBy(
                    $"{getActorWithStrategyDto.OrderByProperty} {getActorWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.ActorRepository.GetWithStrategy(strategy, orderBy, page: getActorWithStrategyDto.Page, getActorWithStrategyDto.PageSize);
            var actors = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
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
            _unitOfWork.ActorRepository.Update(actor);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
