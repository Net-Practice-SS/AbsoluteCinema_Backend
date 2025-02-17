using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Infrastructure.DbContexts;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using AbsoluteCinema.Application.DTO.GenresDTO;
using AbsoluteCinema.Application.DTO.MoviesDTO;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.ActorsDTO;
using AbsoluteCinema.Infrastructure.Converters;
using MovieActorDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieActorDto;
using MovieGenreDto = AbsoluteCinema.Application.DTO.EntityDTO.MovieGenreDto;

namespace AbsoluteCinema.Infrastructure.Seeders
{
    public static class TmdbSeeder
    {
        public static async Task SeedTmdbDataAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            var tmdbService = scope.ServiceProvider.GetRequiredService<ITmdbService>();
            var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
            var movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();
            var actorService = scope.ServiceProvider.GetRequiredService<IActorService>();

            if (!context.Movies.Any())
            {
                var tmdbGenres = await tmdbService.GetGenresAsync();
                var tmdbMovies = (await tmdbService.GetMoviesAsync()).Take(10);

                foreach (var tmdbMovie in tmdbMovies)
                {
                    var createMovieDto = mapper.Map<CreateMovieDto>(tmdbMovie);
                    createMovieDto.TrailerPath = await tmdbService.GetMovieTrailerAsync(tmdbMovie.Id);
                    var movieId = await movieService.CreateMovieAsync(createMovieDto);

                    foreach (var tmdbGenreId in tmdbMovie.GenreIds)
                    {
                        var tmdbGenre = tmdbGenres.FirstOrDefault(tg => tg.Id == tmdbGenreId);

                        var existsGenreDto = (await genreService.GetGenreWithStrategyAsync(new GetGenreWithStrategyDto() { Title = tmdbGenre!.Name })).FirstOrDefault();

                        if (existsGenreDto == null)
                        {
                            var createGenreDto = mapper.Map<CreateGenreDto>(tmdbGenre);
                            var genreId = await genreService.CreateGenreAsync(createGenreDto);
                            await movieService.AddGenreToMovieAsync(new MovieGenreDto() { GenreId = genreId, MovieId = movieId });
                            continue;
                        }

                        await movieService.AddGenreToMovieAsync(new MovieGenreDto() { GenreId = existsGenreDto.Id, MovieId = movieId });
                    }

                    var actors = await tmdbService.GetActorsAsync(tmdbMovie.Id);
                    foreach (var tmdbActor in actors)
                    {
                        var existsActorDto = (await actorService.GetActorWithStrategyAsync(new GetActorWithStrategyDto()
                        {
                            FirstName = FullNameConverter.GetFirstName(tmdbActor.Name),
                            LastName = FullNameConverter.GetLastName(tmdbActor.Name)
                        })).FirstOrDefault();

                        if (existsActorDto == null)
                        {
                            var createActorDto = mapper.Map<CreateActorDto>(tmdbActor);
                            var actorId = await actorService.CreateActorAsync(createActorDto);
                            await movieService.AddActorToMovieAsync(new MovieActorDto()
                            {
                                ActorId = actorId,
                                MovieId = movieId,
                                CharacterName = tmdbActor.Character!
                            });
                            continue;
                        }

                        if (!context.MovieActors.Any(ma => ma.MovieId == movieId && ma.ActorId == existsActorDto.Id))
                        {
                            await movieService.AddActorToMovieAsync(new MovieActorDto()
                            {
                                ActorId = existsActorDto.Id,
                                MovieId = movieId,
                                CharacterName = tmdbActor.Character!
                            });
                        }
                    }
                }
            }
        }
    }
}
