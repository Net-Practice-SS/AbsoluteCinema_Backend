using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var movieDto = await _movieService.GetMovieByIdAsync(id);
            return Ok(movieDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieAll([FromQuery] GetAllMoviesDto getAllMoviesDto)
        {
            var moviesDto = await _movieService.GetAllMoviesAsync(getAllMoviesDto);
            return Ok(moviesDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "Admin")]
        public async Task<ActionResult> CreateMovie([FromForm]CreateMovieDto movieDto)
        {
            var id = await _movieService.CreateMovieAsync(movieDto);
            return Ok(id);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "Admin")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy = "Admin")]
        public async Task<ActionResult> UpdateMovie([FromForm]UpdateMovieDto updateMovieDto)
        {
            await _movieService.UpdateMovieAsync(updateMovieDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieWithStrategy([FromQuery] GetMovieWithStrategyDto getMovieWithStrategyDto)
        {
            var movies = await _movieService.GetMovieWithStrategyAsync(getMovieWithStrategyDto);
            return Ok(movies);
        }

        [HttpGet]
        public async Task<ActionResult> AddGenreToMovie([FromQuery] MovieGenreDto movieGenreDto)
        {
            await _movieService.AddGenreToMovieAsync(movieGenreDto);
            return Ok($"Genre with id: {movieGenreDto.GenreId} added to movie with id: {movieGenreDto.MovieId}");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGenreFromMovie([FromQuery] MovieGenreDto movieGenreDto)
        {
            await _movieService.DeleteGenreFromMovieAsync(movieGenreDto);
            return Ok($"Genre with id: {movieGenreDto.GenreId} deleted from movie with id {movieGenreDto.MovieId}");
        }
    }
}