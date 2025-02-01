using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
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
        public async Task<ActionResult> GetMovieById(int id)
        {
            var movieDto = await _movieService.GetMovieByIdAsync(id);
            return Ok(movieDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieAll()
        {
            var moviesDto = await _movieService.GetAllMoviesAsync();
            return Ok(moviesDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie([FromForm]MovieDto movieDto)
        {
            var id = await _movieService.CreateMovieAsync(movieDto);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMovie([FromForm]MovieDto movieDto)
        {
            await _movieService.UpdateMovieAsync(movieDto);
            return Ok();
        }
    }
}