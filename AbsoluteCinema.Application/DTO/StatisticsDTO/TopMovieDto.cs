namespace AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto
{
    public class TopMovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public double Popularity { get; set; } // От 0 до 1 (0% - 100%)
    }
}
