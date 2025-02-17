using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
    public async Task<ActionResult> CreateSession([FromBody] CreateSessionDto createTicketDto)
    {
        var id = await _sessionService.CreateSessionAsync(createTicketDto);
        return Ok(id);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
    public async Task<ActionResult> DeleteSession(int id)
    {
        await _sessionService.DeleteSessionAsync(id);
        return Ok();
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
    public async Task<ActionResult> UpdateSession([FromBody] UpdateSessionDto updateTicketDto)
    {
        await _sessionService.UpdateSessionAsync(updateTicketDto);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetSessionWithStrategy([FromQuery] GetSessionWithStrategyDto getSessionWithStrategyDto)
    {
        var tickets = await _sessionService.GetSessionWithStrategyAsync(getSessionWithStrategyDto);
        return Ok(tickets);
    }

    [HttpGet]
    public async Task<ActionResult> GetSessionByDate(DateTime date)
    {
        var tickets = await _sessionService.GetSessionsByDateAsync(date);
        return Ok(tickets);
    }

    [HttpGet]
    public async Task<ActionResult> GetSessionsForAdmin([FromBody] GetAllSessionDto getAllSessionDto)
    {
        var tickets = await _sessionService.GetAllSessionsWithIncludeAsync(getAllSessionDto);
        return Ok(tickets);
    }

    [HttpGet]
    public async Task<ActionResult> GetUpcomingSessionsByMovie(int movieId)
    {
        var tickets = await _sessionService.GetUpcomingSessionsByMovieAsync(movieId);
        return Ok(tickets);
    }
}

