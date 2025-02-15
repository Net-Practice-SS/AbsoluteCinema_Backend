using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers;

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
        if (sessionDto == null)
            return NotFound();
        return Ok(sessionDto);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllSessions([FromQuery] GetAllSessionDto getAllTicketsDto)
    {
        var tickets = await _sessionService.GetAllSessionsAsync(getAllTicketsDto);
        return Ok(tickets);
    }

    [HttpPost]
    public async Task<ActionResult> CreateSession([FromForm] CreateSessionDto createTicketDto)
    {
        var id = await _sessionService.CreateSessionAsync(createTicketDto);
        return Ok(id);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSession(int id)
    {
        await _sessionService.DeleteSessionAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateSession([FromForm] UpdateSessionDto updateTicketDto)
    {
        await _sessionService.UpdateSessionAsync(updateTicketDto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetSessionWithStrategy([FromQuery] GetSessionWithStrategyDto getTicketWithStrategyDto)
    {
        var tickets = await _sessionService.GetSessionWithStrategyAsync(getTicketWithStrategyDto);
        return Ok(tickets);
    }

    [HttpGet]
    public async Task<ActionResult> GetSessionByDate(DateTime date)
    {
        var tickets = await _sessionService.GetSessionsByDateAsync(date);
        return Ok(tickets);
    }
}

