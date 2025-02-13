using System.Text.Json.Serialization;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbCastDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
     
        [JsonPropertyName("character")]
        public string? Character { get; set; }
    }
}
