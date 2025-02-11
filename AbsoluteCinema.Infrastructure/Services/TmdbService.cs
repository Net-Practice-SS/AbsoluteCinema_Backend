using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO;
using AbsoluteCinema.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AbsoluteCinema.Infrastructure.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TheMovieDatabase:ApiKey"]!;
            _baseUrl = configuration["TheMovieDatabase:BaseUrl"]!;
        }

        private async Task<T?> GetFromTmdbAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}?api_key={_apiKey}&language=en-US");
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync(int movieId)
        {
            var data = await GetFromTmdbAsync<TmdbActorsDto>($"movie/{movieId}/credits");
            return data?.Actors ?? new List<Actor>();
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            var data = await GetFromTmdbAsync<TmdbGenresDto>("genre/movie/list");
            return data?.Genres ?? new List<Genre>();
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(int page = 1)
        {
            var data = await GetFromTmdbAsync<TmdbMoviesDto>($"movie/popular&page={page}");
            return data?.Movies ?? new List<Movie>();
        }
    }
}
