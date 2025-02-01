using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.WebAPI.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
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
        public async Task<ActionResult> CreateMovie([FromForm]CreateMovieDto movieDto)
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
        public async Task<ActionResult> UpdateMovie([FromForm]UpdateMovieDto updateMovieDto)
        {
            await _movieService.UpdateMovieAsync(updateMovieDto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieWithStrategy([FromQuery]GetMovieWithStrategyDto getMovieWithStrategyDto)
        {
            var movies = await _movieService.GetMovieWithStrategyAsync(getMovieWithStrategyDto);
            return Ok(movies);
        }
    }
}