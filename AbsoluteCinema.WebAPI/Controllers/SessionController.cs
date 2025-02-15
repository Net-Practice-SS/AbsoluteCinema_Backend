using AbsoluteCinema.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using AbsoluteCinema.Application.DTO.SessionsDTO;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSessionById(int id)
        {
            var sessionDto = await _sessionService.GetSessionByIdAsync(id);
            return Ok(sessionDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSessions([FromQuery] GetAllSessionDto getDto)
        {
            var sessionsDto = await _sessionService.GetAllSessionsAsync(getDto);
            return Ok(sessionsDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSession([FromForm] CreateSessionDto createSessionDto)
        {
            var id = await _sessionService.CreateSessionAsync(createSessionDto);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSession(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSession([FromForm] UpdateSessionDto updateSessionDto)
        {
            await _sessionService.UpdateSessionAsync(updateSessionDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetSessionWithStrategy([FromQuery] GetSessionWithStrategyDto getSessionWithStrategyDto)
        {
            var sessions = await _sessionService.GetSessionWithStrategyAsync(getSessionWithStrategyDto);
            return Ok(sessions);
        }
    }
}
