using AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto;
using AbsoluteCinema.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

public class StatisticsController : BaseController
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("revenue")]
    public async Task<ActionResult<decimal>> GetRevenueAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var revenue = await _statisticsService.GetRevenueAsync(startDate, endDate);
        return Ok(revenue);
    }

    [HttpGet("top-movies")]
    public async Task<ActionResult<IEnumerable<TopMovieDto>>> GetTopMoviesByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, int quantityOfMovie)
    {
        var movies = await _statisticsService.GetTopMoviesByPeriodAsync(startDate, endDate, quantityOfMovie);
        return Ok(movies);
    }

    [HttpGet("popular-hall")]
    public async Task<ActionResult<HallDto>> GetMostPopularHall(DateTime startDate, DateTime endDate)
    {
        var hall = await _statisticsService.GetMostPopularHallAsync(startDate, endDate);
        return Ok(hall);
    }

    [HttpGet("busiest-days")]
    public async Task<ActionResult<IEnumerable<WeekdayDto>>> GetBusiestDays()
    {
        var days = await _statisticsService.GetBusiestDaysAsync();
        return Ok(days);
    }
}
