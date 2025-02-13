using System.Text.Json.Serialization;

namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbVideoDto
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = null!;
        
        [JsonPropertyName("site")]
        public string? Site { get; set; }
        
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
