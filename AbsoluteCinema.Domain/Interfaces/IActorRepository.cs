using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IActorRepository : IRepository<Actor>
    {
        void AddMovieToActor(MovieActor movieActor);
        void DeleteMovieFromActor(MovieActor movieActor);
    }
}
