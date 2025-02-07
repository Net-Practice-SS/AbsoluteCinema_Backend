using AbsoluteCinema.Domain.Entities;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        void AddMovieToGenre(MovieGenre movieGenre);
        void DeleteMovieFromGenre(MovieGenre movieGenre);
    }
}
