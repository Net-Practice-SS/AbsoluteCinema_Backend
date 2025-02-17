namespace AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto
{
    public class HallDto
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public double Popularity { get; set; } // От 0 до 1 (0% - 100%)
    }
}
