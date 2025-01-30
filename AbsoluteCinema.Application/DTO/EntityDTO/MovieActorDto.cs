namespace AbsoluteCinema.Application.DTO.Entities;

public class MovieActorDto
{
    public int MovieId { get; set; }
    public int ActorId { get; set; }
    public string CharacterName { get; set; } = null!;
}