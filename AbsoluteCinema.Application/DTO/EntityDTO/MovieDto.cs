using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Application.DTO.Entities;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Discription { get; set; }
    public double? Score { get; set; }
    public bool Adult { get; set; }
    public string? PosterPath { get; set; }
    public MovieLanguageEnum Language { get; set; }
    public DateTime? ReleaseDate { get; set; }
}