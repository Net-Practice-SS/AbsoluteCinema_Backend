using AbsoluteCinema.Domain.Entities.Abstract;
using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Discription { get; set; }
        public double? Score { get; set; }
        public bool? Adult { get; set; }
        public string? PosterPath { get; set; }
        public MovieLanguageEnum Language { get; set; }
        public DateTime? ReleaseDate { get; set; } // New field, we have the data for it in TMDB
        public string? TrailerPath { get; set; }

        public ICollection<MovieGenre> MovieGenre { get; set; } = new List<MovieGenre>();
        public ICollection<MovieActor> MovieActor { get; set; } = new List<MovieActor>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
