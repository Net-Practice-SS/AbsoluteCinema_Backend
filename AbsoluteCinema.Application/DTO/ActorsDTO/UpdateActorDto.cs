namespace AbsoluteCinema.Application.DTO.ActorsDTO
{

    public class UpdateActorDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
    }
}
