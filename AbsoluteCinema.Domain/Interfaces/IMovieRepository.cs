using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void AddGenreToMovie(MovieGenre movieGenre);
        void DeleteGenreFromMovie(MovieGenre movieGenre);

        void AddActorToMovie(MovieActor movieActor);
        void DeleteActorFromMovie(MovieActor movieActor);
    }
}
