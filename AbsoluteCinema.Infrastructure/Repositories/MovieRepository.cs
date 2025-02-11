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
        
        public void AddActorToMovie(MovieActor movieActor)
        {
            var movie = _dbContext.Movies.Find(movieActor.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieActor.MovieId.ToString());

            var actor = _dbContext.Actors.Find(movieActor.ActorId);
            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", movieActor.ActorId.ToString());

            if (_dbContext.MovieActors.Any(mg => mg.MovieId == movieActor.MovieId && mg.ActorId == movieActor.ActorId))
                throw new AlreadyExistEntityException(nameof(MovieActor), "(MovieId, ActorId)", $"{movieActor.MovieId}, {movieActor.ActorId}");

            movieActor.Movie = movie;
            movieActor.Actor = actor;

            actor.MovieActor.Add(movieActor);
        }
        
        public void DeleteActorFromMovie(MovieActor movieActor)
        {
            var movie = _dbContext.Movies.Find(movieActor.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieActor.MovieId.ToString());

            var actor = _dbContext.Actors.Find(movieActor.ActorId);
            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", movieActor.ActorId.ToString());

            if (!_dbContext.MovieActors.Any(mg => mg.MovieId == movieActor.MovieId && mg.ActorId == movieActor.ActorId))
                throw new EntityNotFoundException(nameof(MovieActor), "(MovieId, ActorId)", $"{movieActor.MovieId}, {movieActor.ActorId}");

            movieActor.Movie = movie;
            movieActor.Actor = actor;

            _dbContext.MovieActors.Remove(movieActor);
        }
    }
}
