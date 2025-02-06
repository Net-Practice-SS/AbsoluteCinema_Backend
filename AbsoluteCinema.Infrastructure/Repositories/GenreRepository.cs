using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context) { }

        public void AddMovieToGenre(MovieGenre movieGenre)
        {
            var genre = _dbContext.Genres.Find(movieGenre.GenreId);
            if (genre == null)
                throw new EntityNotFoundException(nameof(Genre), "Id", movieGenre.GenreId.ToString());

            var movie = _dbContext.Movies.Find(movieGenre.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieGenre.MovieId.ToString());

            movieGenre.Movie = movie;
            movieGenre.Genre = genre;

            genre.MovieGenre.Add(movieGenre);
        }

        public void DeleteMovieFromGenre(MovieGenre movieGenre)
        {
            var genre = _dbContext.Genres.Find(movieGenre.GenreId);
            if (genre == null)
                throw new EntityNotFoundException(nameof(Genre), "Id", movieGenre.GenreId.ToString());

            var movie = _dbContext.Movies.Find(movieGenre.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieGenre.MovieId.ToString());

            movieGenre.Movie = movie;
            movieGenre.Genre = genre;

            _dbContext.MovieGenre.Remove(movieGenre);
        }
    }
}
