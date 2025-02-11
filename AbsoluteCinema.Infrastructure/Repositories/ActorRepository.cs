using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Infrastructure.DbContexts;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(AppDbContext context) : base(context) { }
        
        public void AddMovieToActor(MovieActor movieActor)
        {
            var actor = _dbContext.Actors.Find(movieActor.ActorId);
            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", movieActor.ActorId.ToString());

            var movie = _dbContext.Movies.Find(movieActor.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieActor.MovieId.ToString());

            if (_dbContext.MovieActors.Any(mg => mg.MovieId == movieActor.MovieId && mg.ActorId == movieActor.ActorId))
                throw new AlreadyExistEntityException(nameof(MovieActor), "(MovieId, ActorId)", $"{movieActor.MovieId}, {movieActor.ActorId}");

            movieActor.Movie = movie;
            movieActor.Actor = actor;

            actor.MovieActor.Add(movieActor);
        }
        
        public void DeleteMovieFromActor(MovieActor movieActor)
        {
            var actor = _dbContext.Actors.Find(movieActor.ActorId);
            if (actor == null)
                throw new EntityNotFoundException(nameof(Actor), "Id", movieActor.ActorId.ToString());

            var movie = _dbContext.Movies.Find(movieActor.MovieId);
            if (movie == null)
                throw new EntityNotFoundException(nameof(Movie), "Id", movieActor.MovieId.ToString());

            if (!_dbContext.MovieActors.Any(mg => mg.MovieId == movieActor.MovieId && mg.ActorId == movieActor.ActorId))
                throw new EntityNotFoundException(nameof(MovieActor), "(MovieId, ActorId)", $"{movieActor.MovieId}, {movieActor.ActorId}");

            movieActor.Movie = movie;
            movieActor.Actor = actor;

            _dbContext.MovieActors.Remove(movieActor);
        }
    }
}
