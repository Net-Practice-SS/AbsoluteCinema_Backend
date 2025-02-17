using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.GenresDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult> GetGenreById(int id)
        {
            var genreDto = await _genreService.GetGenreByIdAsync(id);
            return Ok(genreDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetGenreAll([FromQuery] GetAllGenreDto getAllGenreDto)
        {
            var genresDto = await _genreService.GetAllGenresAsync(getAllGenreDto);
            return Ok(genresDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> CreateGenre([FromBody] CreateGenreDto createGenreDto)
        {
            var id = await _genreService.CreateGenreAsync(createGenreDto);
            return Ok(id);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> AddMovieToGenre([FromQuery] MovieGenreDto movieGenreDto)
        {
            await _genreService.AddMovieToGenreAsync(movieGenreDto);
            return Ok($"Movie with id: {movieGenreDto.MovieId} added to genre with id: {movieGenreDto.GenreId}");
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return Ok($"Genre with id: {id} successfully deleted");
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> DeleteMovieFromGenre([FromQuery] MovieGenreDto movieGenreDto)
        {
            await _genreService.DeleteMovieFromGenreAsync(movieGenreDto);
            return Ok($"Movie with id: {movieGenreDto.MovieId} deleted from genre with id: {movieGenreDto.GenreId}");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> UpdateGenre([FromBody] UpdateGenreDto updateGenreDto)
        {
            await _genreService.UpdateGenreAsync(updateGenreDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetGenreWithStrategy([FromQuery] GetGenreWithStrategyDto getGenreWithStrategyDto)
        {
            var genres = await _genreService.GetGenreWithStrategyAsync(getGenreWithStrategyDto);
            return Ok(genres);
        }
    }
}
