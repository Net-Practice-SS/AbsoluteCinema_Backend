using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieGenreDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieGenreDto;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> CreateMovie([FromBody] CreateMovieDto movieDto)
        {
            var id = await _movieService.CreateMovieAsync(movieDto);
            return Ok(id);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> AddGenreToMovie([FromBody] MovieGenreDto movieGenreDto)
        {
            await _movieService.AddGenreToMovieAsync(movieGenreDto);
            return Ok($"Genre with id: {movieGenreDto.GenreId} added to movie with id: {movieGenreDto.MovieId}");
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policy.AdminPolicy)]
        public async Task<ActionResult> DeleteGenreFromMovie([FromBody] MovieGenreDto movieGenreDto)
        {
            await _movieService.DeleteGenreFromMovieAsync(movieGenreDto);
            return Ok($"Genre with id: {movieGenreDto.GenreId} deleted from movie with id {movieGenreDto.MovieId}");
        }
        
        [HttpGet]
        public async Task<ActionResult> GetAllMoviesForAdmin()
        {
            var movies = await _movieService.GetAllMoviesWithIncludeAsync();
            return Ok(movies);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetPersonalizedMovieSuggestions(int userId)
        {
            var movies = await _movieService.GetPersonalizedMovieSuggestionsAsync(userId);
            return Ok(movies);
        }
    }
}