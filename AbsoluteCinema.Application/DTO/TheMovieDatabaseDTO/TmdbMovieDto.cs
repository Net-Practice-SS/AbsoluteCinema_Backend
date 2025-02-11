namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbMovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? PosterPath { get; set; }
        public double VoteAverage { get; set; }
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();

        public string? FullPosterPath => PosterPath != null
            ? $"https://image.tmdb.org/t/p/w500{PosterPath}"
            : null;
    }
}
