﻿using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Application.DTO.MoviesDTO
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Discription { get; set; } = null!;
        public double? Score { get; set; } = null!;
        public bool? Adult { get; set; } = null!;
        public string? PosterPath { get; set; } = null!;
        public MovieLanguageEnum? Language { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; } = null!;
        public string? TrailerPath { get; set; } = null!;
    }
}
