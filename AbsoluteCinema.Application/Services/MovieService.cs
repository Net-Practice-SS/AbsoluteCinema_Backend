using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AutoMapper;

namespace AbsoluteCinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateMovieAsync(CreateMovieDto createMovieDto)
        {
            var movieDto = _mapper.Map<MovieDto>(createMovieDto); 
            var movie = _mapper.Map<Movie>(movieDto);
            _unitOfWork.Repository<Movie>().Add(movie);
            await _unitOfWork.SaveChangesAsync();
            return movie.Id;
        }

        public async Task DeleteMovieAsync(int id)
        {
            _unitOfWork.Repository<Movie>().Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _unitOfWork.Repository<Movie>().GetAllAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.Repository<Movie>().GetByIdAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task UpdateMovieAsync(UpdateMovieDto updateMovieDto)
        {
            var currentMovieDto = await GetMovieByIdAsync(updateMovieDto.Id);

            if (currentMovieDto == null)
            {
                throw new KeyNotFoundException("Кіно не знайдено");
            }

            _mapper.Map(updateMovieDto, currentMovieDto);

            var movie = _mapper.Map<Movie>(currentMovieDto);
            _unitOfWork.Repository<Movie>().Update(movie);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
