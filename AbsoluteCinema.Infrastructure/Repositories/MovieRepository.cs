using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void AddGenreToMovie(MovieGenre movieGenre)
        {
            var movie = _dbContext.Movies.Find(movieGenre.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieGenre.MovieId.ToString());

            var genre = _dbContext.Genres.Find(movieGenre.GenreId);
            if (genre == null)
                throw new EntityNotFoundException(nameof(Genre), "Id", movieGenre.GenreId.ToString());

            if (_dbContext.MovieGenre.Any(mg => mg.MovieId == movieGenre.MovieId && mg.GenreId == movieGenre.GenreId))
                throw new AlreadyExistEntityException(nameof(MovieGenre), "(MovieId, GenreId)", $"({movieGenre.MovieId}, {movieGenre.GenreId})");

            movieGenre.Movie = movie;
            movieGenre.Genre = genre;

            genre.MovieGenre.Add(movieGenre);
        }

        public void DeleteGenreFromMovie(MovieGenre movieGenre)
        {
            var movie = _dbContext.Movies.Find(movieGenre.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieGenre.MovieId.ToString());

            var genre = _dbContext.Genres.Find(movieGenre.GenreId);
            if (genre == null)
                throw new EntityNotFoundException(nameof(Genre), "Id", movieGenre.GenreId.ToString());

            if (!_dbContext.MovieGenre.Any(mg => mg.MovieId == movieGenre.MovieId && mg.GenreId == movieGenre.GenreId))
                throw new EntityNotFoundException(nameof(MovieGenre), "(MovieId, GenreId)", $"{movieGenre.MovieId}, {movieGenre.GenreId}");

            movieGenre.Movie = movie;
            movieGenre.Genre = genre;

            _dbContext.MovieGenre.Remove(movieGenre);
        }
    }
}
