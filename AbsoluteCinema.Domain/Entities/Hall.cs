using AbsoluteCinema.Domain.Entities.Abstract;

namespace AbsoluteCinema.Domain.Entities
{
    public class Hall : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int RowCount { get; set; }
        public int PlaceCount { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
