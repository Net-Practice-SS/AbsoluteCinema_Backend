using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using AbsoluteCinema.Application.DTO.Entities;


namespace AbsoluteCinema.Application.Contracts
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionDto>> GetAllSessionsAsync(GetAllSessionDto getAllSessionsDto);
        Task<SessionDto?> GetSessionByIdAsync(int id);
        Task DeleteSessionAsync(int id);
        Task UpdateSessionAsync(UpdateSessionDto updateSessionDto);
        Task<int> CreateSessionAsync(CreateSessionDto sessionDto);
        Task<IEnumerable<SessionDto>> GetSessionWithStrategyAsync(GetSessionWithStrategyDto getSessionWithStrategyDto);
        Task<IEnumerable<SessionDto>> GetSessionsByDateAsync(DateTime date);
        Task<IEnumerable<SessionDto>> GetAllSessionsWithIncludeAsync(GetAllSessionDto getAllSessionsDto);
        Task<IEnumerable<SessionDto>> GetUpcomingSessionsByMovieAsync(int movieId);
    }
}
