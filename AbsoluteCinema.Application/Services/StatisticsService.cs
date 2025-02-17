using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.AuthDTO.StatisticsDto;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisticsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<double> GetRevenueAsync(DateTime? startDate, DateTime? endDate)
        {
            var tickets = await _unitOfWork.Repository<Ticket>()
            .GetAllAsync(include: query => query.Include(t => t.Session));

            if (startDate.HasValue && endDate.HasValue)
            {
                tickets = tickets.Where(t => t.Session.Date >= startDate.Value && t.Session.Date <= endDate.Value);
            }
            else
            {
                tickets = tickets.Where(t => t.Session.Date >= DateTime.MinValue && t.Session.Date <= DateTime.Now);
            }

            return tickets.Sum(t => t.Price);
        }

        public async Task<IEnumerable<TopMovieDto>> GetTopMoviesByPeriodAsync(DateTime startDate, DateTime endDate, int quantityOfMoviesInTop = 10)
        {
            var tickets = await _unitOfWork.Repository<Ticket>()
               .GetAllAsync(include: query => query.Include(t => t.Session).ThenInclude(g => g.Hall));

            var query = tickets
                .Where(t => t.Session.Date >= startDate && t.Session.Date <= endDate)
                .GroupBy(t => t.Session.MovieId)
                .Select(g => new
                {
                    MovieId = g.Key,
                    Popularity = Math.Round((double)g.Count() / (g.FirstOrDefault().Session.Hall.PlaceCount * g.FirstOrDefault().Session.Hall.RowCount), 3)
                })
                .OrderByDescending(m => m.Popularity)
                .Take(quantityOfMoviesInTop);

            // Выполняем запрос и получаем данные
            var movieData = query.ToList();

            // Получаем данные о фильмах
            var movieIds = movieData.Select(m => m.MovieId).ToList();
            var movies = await _unitOfWork.Repository<Movie>().GetAllAsync();

            // Извлекаем названия фильмов по их Id
            var movieTitles = movies
                .Where(m => movieIds.Contains(m.Id))
                .ToDictionary(m => m.Id, m => m.Title);

            // Формируем конечный список DTO с названиями
            var result = movieData.Select(m => new TopMovieDto
            {
                MovieId = m.MovieId,
                Title = movieTitles[m.MovieId],
                Popularity = m.Popularity
            }).ToList();

            return result;
        }

        public async Task<HallDto> GetMostPopularHallAsync(DateTime startDate, DateTime endDate)
        {
            var tickets = await _unitOfWork.Repository<Ticket>()
               .GetAllAsync(include: query => query.Include(t => t.Session).ThenInclude(g => g.Hall));

            var query = tickets
                .Where(t => t.Session.Date >= startDate && t.Session.Date <= endDate)
                .GroupBy(t => t.Session.HallId)
                .Select(g => new
                {
                    HallId = g.Key,
                    HallName = g.FirstOrDefault().Session.Hall.Name,
                    Popularity = Math.Round((double)g.Count() / (g.FirstOrDefault().Session.Hall.PlaceCount * g.FirstOrDefault().Session.Hall.RowCount), 3)
                })
                .OrderByDescending(h => h.Popularity)
                .FirstOrDefault();

            return new HallDto
            {
                HallId = query.HallId,
                Name = query.HallName,
                Popularity = query.Popularity
            };
        }

        public async Task<IEnumerable<WeekdayDto>> GetBusiestDaysAsync()
        {
            var tickets = await _unitOfWork.Repository<Ticket>()
            .GetAllAsync(include: query => query.Include(t => t.Session));

            var query = tickets
                .GroupBy(t => t.Session.Date.DayOfWeek)
                .Select(g => new WeekdayDto
                {
                    DayOfWeek = g.Key.ToString(),
                    TicketsSold = g.Count()
                });

            var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            var result = allDaysOfWeek.Select(day =>
            {
                var dayStats = query.FirstOrDefault(q => q.DayOfWeek == day.ToString());
                return new WeekdayDto
                {
                    DayOfWeek = day.ToString(),
                    TicketsSold = dayStats?.TicketsSold ?? 0
                };
            });

            return result.OrderBy(day => (int)Enum.Parse(typeof(DayOfWeek), day.DayOfWeek));
        }
    }
}
