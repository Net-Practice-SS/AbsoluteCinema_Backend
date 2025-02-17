using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Strategies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using MovieActorDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieActorDto;
using MovieGenreDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieGenreDto;

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

        public async Task AddGenreToMovieAsync(MovieGenreDto movieGenreDto)
        {
            var movieGenre = _mapper.Map<MovieGenre>(movieGenreDto);
            _unitOfWork.MovieRepository.AddGenreToMovie(movieGenre);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task AddActorToMovieAsync(MovieActorDto movieActorDto)
        {
            var movieActor = _mapper.Map<MovieActor>(movieActorDto);
            _unitOfWork.MovieRepository.AddActorToMovie(movieActor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> CreateMovieAsync(CreateMovieDto createMovieDto)
        {
            var movieDto = _mapper.Map<MovieDto>(createMovieDto);
            var movie = _mapper.Map<Movie>(movieDto);
            _unitOfWork.MovieRepository.Add(movie);
            await _unitOfWork.SaveChangesAsync();
            return movie.Id;
        }

        public async Task DeleteGenreFromMovieAsync(MovieGenreDto movieGenreDto)
        {
            var movieGenre = _mapper.Map<MovieGenre>(movieGenreDto);
            _unitOfWork.MovieRepository.DeleteGenreFromMovie(movieGenre);
            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteActorFromMovieAsync(MovieActorDto movieActorDto)
        {
            var movieActor = _mapper.Map<MovieActor>(movieActorDto);
            _unitOfWork.MovieRepository.DeleteActorFromMovie(movieActor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            _unitOfWork.MovieRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync(GetAllMoviesDto getAllMoviesDto)
        {
            // Будуємо делегат orderBy використовуючи динамічний LINQ
            Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy =
                query => query.OrderBy($"{getAllMoviesDto.OrderByProperty} {getAllMoviesDto.OrderDirection}");

            var movies = await _unitOfWork.MovieRepository.GetAllAsync(orderBy, include: null, page: getAllMoviesDto.Page, getAllMoviesDto.PageSize);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _unitOfWork.MovieRepository.GetByIdAsync(id);

            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", id.ToString());

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<IEnumerable<MovieDto>> GetMovieWithStrategyAsync(GetMovieWithStrategyDto getMovieWithStrategyDto)
        {
            var strategy = new MovieStrategy(
                getMovieWithStrategyDto.Title,
                getMovieWithStrategyDto.Discription,
                getMovieWithStrategyDto.Score,
                getMovieWithStrategyDto.Adult,
                getMovieWithStrategyDto.Language,
                getMovieWithStrategyDto.ReleaseDateFrom,
                getMovieWithStrategyDto.ReleaseDateTo,
                getMovieWithStrategyDto.ActorsIds,
                getMovieWithStrategyDto.GenresIds);

            // Будуємо делегат orderBy використовуючи динамічний LINQ
            Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderBy = 
                query => query.OrderBy($"{getMovieWithStrategyDto.OrderByProperty} {getMovieWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.MovieRepository.GetWithStrategy(strategy, orderBy, page: getMovieWithStrategyDto.Page, getMovieWithStrategyDto.PageSize);
            var movies = await query.ToListAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task UpdateMovieAsync(UpdateMovieDto updateMovieDto)
        {
            var currentMovieDto = await GetMovieByIdAsync(updateMovieDto.Id);

            if (currentMovieDto == null)
            {
                throw new EntityNotFoundException(nameof(Movie), "Id", updateMovieDto.Id.ToString());
            }

            _mapper.Map(updateMovieDto, currentMovieDto);

            var movie = _mapper.Map<Movie>(currentMovieDto);
            _unitOfWork.MovieRepository.Update(movie);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<MovieDto>> GetAllMoviesWithIncludeAsync()
        {
            var movieDto = await _unitOfWork.Repository<Movie>().GetAllAsync(
                include: query => query
                    .Include(s => s.MovieGenre)
                        .ThenInclude(mg => mg.Genre)
                    .Include(s => s.MovieActor)
                        .ThenInclude(ma => ma.Actor)
            );
            
            var movies = _mapper.Map<IEnumerable<MovieDto>>(movieDto);
            return movies;
        }
        
        // Этот метод был сделан за 1 запрос в chatGpt, я задумываюсь стоит ли мне дальше быть программистом.
        public async Task<IEnumerable<MovieDto>> GetPersonalizedMovieSuggestionsAsync(int userId)
        {
            var userTickets = await _unitOfWork.Repository<Ticket>().GetAllAsync(
                include: query => query
                    .Where(t => t.UserId == userId)
                    .Include(t => t.Session)
                        .ThenInclude(s => s.Movie)
                        .ThenInclude(m => m.MovieGenre)
            );
            
            var watchedMovies = userTickets
                .Select(t => t.Session.Movie)
                .Distinct()
                .ToList();
            
            var genreIds = watchedMovies
                .SelectMany(m => m.MovieGenre.Select(mg => mg.GenreId))
                .Distinct()
                .ToList();
            
            if (!genreIds.Any())
            {
                return new List<MovieDto>();
            }
            
            var suggestedMovies = await _unitOfWork.Repository<Movie>().GetAllAsync(
                include: query => query
                    .Include(m => m.MovieGenre),
                orderBy: query => query.OrderBy(m => m.Score)
            );
            
            suggestedMovies = suggestedMovies
                .Where(m => m.MovieGenre.Any(mg => genreIds.Contains(mg.GenreId)));
            
            suggestedMovies = suggestedMovies
                .Where(m => !watchedMovies.Any(w => w.Id == m.Id));
            
            var result = suggestedMovies.Take(5);
            
            return _mapper.Map<IEnumerable<MovieDto>>(result);
        }

    }
}
