using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.DTO.Entities;
using AbsoluteCinema.Application.DTO.EntityDTO;
using AbsoluteCinema.Application.DTO.GenresDTO;
using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Exceptions;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Domain.Strategies;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AbsoluteCinema.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddMovieToGenreAsync(MovieGenreDto movieGenreDto)
        {
            var movieGenre = _mapper.Map<MovieGenre>(movieGenreDto);
            _unitOfWork.GenreRepository.AddMovieToGenre(movieGenre);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            var genreDto = _mapper.Map<GenreDto>(createGenreDto);
            var genre = _mapper.Map<Genre>(genreDto);
            _unitOfWork.GenreRepository.Add(genre);
            await _unitOfWork.SaveChangesAsync();
            return genre.Id;
        }

        public async Task DeleteGenreAsync(int id)
        {
            _unitOfWork.GenreRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMovieFromGenreAsync(MovieGenreDto movieGenreDto)
        {
            var movieGenre = _mapper.Map<MovieGenre>(movieGenreDto);
            _unitOfWork.GenreRepository.DeleteMovieFromGenre(movieGenre);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync(GetAllGenreDto getAllGenreDto)
        {
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy =
                query => query.OrderBy($"{getAllGenreDto.OrderByProperty} {getAllGenreDto.OrderDirection}");

            var genres = await _unitOfWork.GenreRepository.GetAllAsync(orderBy, include: null, page: getAllGenreDto.Page, getAllGenreDto.PageSize);
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto?> GetGenreByIdAsync(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

            if (genre == null)
                throw new EntityNotFoundException(nameof(Genre), "Id", id.ToString());

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<IEnumerable<GenreDto>> GetGenreWithStrategyAsync(GetGenreWithStrategyDto getGenreWithStrategyDto)
        {
            var strategy = new GenreStrategy(
                getGenreWithStrategyDto.Title!,
                getGenreWithStrategyDto.MoviesIds);

            Func<IQueryable<Genre>, IOrderedQueryable<Genre>> orderBy =
                query => query.OrderBy($"{getGenreWithStrategyDto.OrderByProperty} {getGenreWithStrategyDto.OrderDirection}");

            var query = _unitOfWork.GenreRepository.GetWithStrategy(strategy, orderBy, page: getGenreWithStrategyDto.Page, getGenreWithStrategyDto.PageSize);
            var genres = await query.ToListAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task UpdateGenreAsync(UpdateGenreDto updateGenreDto)
        {
            var currentHallDto = await GetGenreByIdAsync(updateGenreDto.Id);

            if (currentHallDto == null)
            {
                throw new EntityNotFoundException(nameof(Genre), "Id", updateGenreDto.Id.ToString());
            }

            _mapper.Map(updateGenreDto, currentHallDto);

            var genre = _mapper.Map<Genre>(currentHallDto);
            _unitOfWork.GenreRepository.Update(genre);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
