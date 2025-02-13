using System.Text.Json.Serialization;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbMovieDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("overview")]
        public string? Overview { get; set; }

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("original_language")]
        public string? OriginalLanguage { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("genre_ids")]
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();

        public string? FullPosterPath => PosterPath != null
            ? $"https://image.tmdb.org/t/p/w500{PosterPath}"
            : null;
    }
}
