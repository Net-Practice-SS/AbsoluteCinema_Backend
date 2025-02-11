using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
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
            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}?api_key={_apiKey}&language=en-US");
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<ActorDto>> GetActorsAsync(int movieId)
        {
            var data = await GetFromTmdbAsync<TmdbCastResponse>($"movie/{movieId}/credits");
            
            if (data?.Cast == null)
            {
                throw new EntityNotFoundException(nameof(TmdbCastResponse), "MovieId", movieId.ToString());
            }

            var entity = _mapper.Map<IEnumerable<ActorDto>>(data.Cast);
            return entity;
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            var data = await GetFromTmdbAsync<TmdbGenreResponse>("genre/movie/list");

            if (data?.Genres == null)
            {
                throw new EntityNotFoundException(nameof(TmdbGenreResponse), string.Empty, string.Empty);
            }

            var entity = _mapper.Map<IEnumerable<GenreDto>>(data.Genres);
            return entity;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(int page = 1)
        {
            var data = await GetFromTmdbAsync<TmdbMovieResponse>($"movie/popular&page={page}");

            if (data?.Results == null)
            {
                throw new EntityNotFoundException(nameof(TmdbMovieResponse), "Page", page.ToString());
            }

            var entity = _mapper.Map<IEnumerable<MovieDto>>(data.Results);
            return entity;
        }
    }
}
