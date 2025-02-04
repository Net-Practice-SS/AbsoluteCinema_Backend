﻿using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.HallsDTO;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class HallController : BaseController
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<ActionResult> GetHallById(int id)
        {
            var hallDto = await _hallService.GetHallByIdAsync(id);
            return Ok(hallDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetHallAll([FromQuery] GetAllHallDto getAllHallsDto)
        {
            var hallsDto = await _hallService.GetAllHallsAsync(getAllHallsDto);
            return Ok(hallsDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateHall([FromForm] CreateHallDto createHallDto)
        {
            var id = await _hallService.CreateHallAsync(createHallDto);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteHall(int id)
        {
            await _hallService.DeleteHallAsync(id);
            return Ok($"Hall with id: {id} successfully deleted");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateHall([FromForm] UpdateHallDto updateHallDto)
        {
            await _hallService.UpdateHallAsync(updateHallDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetHallWithStrategy([FromQuery] GetHallWithStrategyDto getHallWithStrategyDto)
        {
            var halls = await _hallService.GetHallWithStrategyAsync(getHallWithStrategyDto);
            return Ok(halls);
        }
    }
}
