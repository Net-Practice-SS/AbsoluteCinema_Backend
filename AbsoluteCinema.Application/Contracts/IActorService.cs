using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.ActorsDTO;

namespace AbsoluteCinema.Application.Contracts
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetAllActorsAsync(GetAllActorDto getAllActorDto);
        Task<ActorDto?> GetActorByIdAsync(int id);
        Task DeleteActorAsync(int id);
        Task UpdateActorAsync(UpdateActorDto updateActorDto);
        Task<int> CreateActorAsync(CreateActorDto createActorDto);
        Task<IEnumerable<ActorDto>> GetActorWithStrategyAsync(GetActorWithStrategyDto getActorWithStrategyDto);
        Task AddMovieToActorAsync(MovieActorDto movieActorDto);
        Task DeleteMovieFromActorAsync(MovieActorDto movieActorDto);
    }
}
