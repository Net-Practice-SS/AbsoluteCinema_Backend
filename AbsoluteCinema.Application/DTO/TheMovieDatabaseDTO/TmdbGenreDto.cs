using System.Text.Json.Serialization;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbGenreDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
