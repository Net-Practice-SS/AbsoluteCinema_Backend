using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.TheMovieDatabaseDTO;
using AbsoluteCinema.Domain.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AbsoluteCinema.Infrastructure.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly IMapper _mapper;

        public TmdbService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TheMovieDatabase:ApiKey"]!;
            _baseUrl = configuration["TheMovieDatabase:BaseUrl"]!;
            _mapper = mapper;
        }

        private async Task<T?> GetFromTmdbAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}?api_key={_apiKey}&language=en-US&");
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<TmdbCastDto>> GetActorsAsync(int movieId)
        {
            var data = await GetFromTmdbAsync<TmdbCastResponse>($"movie/{movieId}/credits");

            if (data?.Cast == null)
            {
                throw new EntityNotFoundException(nameof(TmdbCastResponse), "MovieId", movieId.ToString());
            }

            return data.Cast;
        }

        public async Task<IEnumerable<TmdbGenreDto>> GetGenresAsync()
        {
            var data = await GetFromTmdbAsync<TmdbGenreResponse>("genre/movie/list");

            if (data?.Genres == null)
            {
                throw new EntityNotFoundException(nameof(TmdbGenreResponse), string.Empty, string.Empty);
            }

            return data.Genres;
        }

        public async Task<IEnumerable<TmdbMovieDto>> GetMoviesAsync(int page = 1)
        {
            var data = await GetFromTmdbAsync<TmdbMovieResponse>($"movie/popular");

            if (data?.Results == null)
            {
                throw new EntityNotFoundException(nameof(TmdbMovieResponse), "Page", page.ToString());
            }

            return data.Results;
        }

        public async Task<string> GetMovieTrailerAsync(int movieId)
        {
            var data = await GetFromTmdbAsync<TmdbVideoResponse>($"movie/{movieId}/videos");
            var trailer = data?.Results.FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube");

            if (trailer == null)
            {
                throw new EntityNotFoundException("Trailer", "MovieId", movieId.ToString());
            }

            return $"https://www.youtube.com/watch?v={trailer.Key}";
        }
    }
}
