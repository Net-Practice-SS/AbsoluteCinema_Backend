namespace AbsoluteCinema.Application.DTO.AuthDTO.SessionsDTO
{
    public class CreateSessionDto
    {
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
    }
}
