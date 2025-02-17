using AbsoluteCinema.Domain.Enums;

namespace AbsoluteCinema.Application.DTO.MoviesDTO
{
    public class GetMovieWithStrategyDto
    {
        public string? Title { get; set; } = null!;
        public string? Discription { get; set; } = null!;
        public double? Score { get; set; } = null!;
        public bool? Adult { get; set; } = null!;
        public string? PosterPath { get; set; } = null!;
        public MovieLanguageEnum? Language { get; set; } = null!;
        public DateTime? ReleaseDateFrom { get; set; } = null!;
        public DateTime? ReleaseDateTo { get; set; } = null!;
        public List<int> ActorsIds { get; set; } = new();
        public List<int> GenresIds { get; set; } = new();

        public string OrderByProperty { get; set; } = "Id"; 
        public string OrderDirection { get; set; } = "asc"; // "asc" або "desc"
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
