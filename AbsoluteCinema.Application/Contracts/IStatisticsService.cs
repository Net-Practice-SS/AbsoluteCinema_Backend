using AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto;

public interface IStatisticsService
{
    Task<double> GetRevenueAsync(DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<TopMovieDto>> GetTopMoviesByPeriodAsync(DateTime startDate, DateTime endDate, int quantityOfMoviesInTop);
    Task<HallDto> GetMostPopularHallAsync();
    Task<IEnumerable<WeekdayDto>> GetBusiestDaysAsync();
}
