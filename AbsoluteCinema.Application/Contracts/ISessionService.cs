using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.SessionsDTO;

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
    }
}
