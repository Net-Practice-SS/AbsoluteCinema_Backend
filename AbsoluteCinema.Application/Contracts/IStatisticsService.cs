using AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto;

public interface IStatisticsService
{
    Task<decimal> GetRevenueAsync(DateTime? startDate, DateTime? endDate);
    Task<IEnumerable<TopMovieDto>> GetTopMoviesByPeriodAsync(DateTime startDate, DateTime endDate);
    Task<HallDto> GetMostPopularHallAsync();
    Task<IEnumerable<WeekdayDto>> GetBusiestDaysAsync();
}
