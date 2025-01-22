namespace AbsoluteCinema.Domain.Entities
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int PlaceCount { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
