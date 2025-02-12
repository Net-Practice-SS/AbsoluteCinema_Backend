namespace AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO
{
    public class TmdbVideoDto
    {
        public string Key { get; set; } = null!;
        public string? Site { get; set; }
        public string? Type { get; set; }
        public bool Official { get; set; }
    }
}
