using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.ActorsDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class ActorController : BaseController
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<ActionResult> GetActorById(int id)
        {
            var actorDto = await _actorService.GetActorByIdAsync(id);
            return Ok(actorDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetActorAll([FromQuery] GetAllActorDto getAllActorsDto)
        {
            var actorsDto = await _actorService.GetAllActorsAsync(getAllActorsDto);
            return Ok(actorsDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> CreateActor([FromBody] CreateActorDto actorDto)
        {
            var id = await _actorService.CreateActorAsync(actorDto);
            return Ok(id);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> DeleteActor(int id)
        {
            await _actorService.DeleteActorAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> UpdateActor([FromBody] UpdateActorDto updateActorDto)
        {
            await _actorService.UpdateActorAsync(updateActorDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetActorWithStrategy(
            [FromQuery] GetActorWithStrategyDto getActorWithStrategyDto)
        {
            var actors = await _actorService.GetActorWithStrategyAsync(getActorWithStrategyDto);
            return Ok(actors);
        }
    }
}
