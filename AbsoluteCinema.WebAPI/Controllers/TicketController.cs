﻿using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.TicketsDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers;

public class TicketController : BaseController
{
    private readonly ITicketService _ticketService;
        
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
        
    [HttpGet]
    public async Task<ActionResult> GetTicketById(int id)
    {
        var ticketDto = await _ticketService.GetTicketByIdAsync(id);
        if (ticketDto == null)
            return NotFound();
        return Ok(ticketDto);
    }
        
    [HttpGet]
    public async Task<ActionResult> GetAllTickets([FromQuery]GetAllTicketsDto getAllTicketsDto)
    {
        var tickets = await _ticketService.GetAllTicketsAsync(getAllTicketsDto);
        return Ok(tickets);
    }
        
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.UserPolicy)]
    public async Task<ActionResult> CreateTicket([FromForm] CreateTicketDto createTicketDto)
    {
        var id = await _ticketService.CreateTicketAsync(createTicketDto);
        return Ok(id);
    }
        
    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
    public async Task<ActionResult> DeleteTicket(int id)
    {
        await _ticketService.DeleteTicketAsync(id);
        return Ok();
    }
        
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
    public async Task<ActionResult> UpdateTicket([FromForm] UpdateTicketDto updateTicketDto)
    {
        await _ticketService.UpdateTicketAsync(updateTicketDto);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult> GetTicketWithStrategy([FromQuery] GetTicketWithStrategyDto getTicketWithStrategyDto)
    {
        var tickets = await _ticketService.GetTicketWithStrategyAsync(getTicketWithStrategyDto);
        return Ok(tickets);
    }
}