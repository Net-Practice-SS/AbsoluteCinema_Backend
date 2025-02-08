namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class UpdateSessionDto
    {
        public int Id { get; set; }
        public int? MovieId { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public int? HallId { get; set; } = null!;
    }
}
