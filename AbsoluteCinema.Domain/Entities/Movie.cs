using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Discription { get; set; }
        public double? Score { get; set; }
        public bool Adult { get; set; }
        public string? PosterPath { get; set; }
        public MovieLanguageEnum Language { get; set; }
        public DateTime? ReleaseDate { get; set; } // New field, we have the data for it in TMDB

        public ICollection<MovieGenre> MovieGenre { get; set; }
        public ICollection<MovieActor> MovieActor { get; set; }
        public ICollection<Session> Sessions { get; set; }  
    }
}
