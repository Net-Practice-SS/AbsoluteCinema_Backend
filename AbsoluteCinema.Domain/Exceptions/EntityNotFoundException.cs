namespace AbsoluteCinema.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, string property, string value)
            : base($"{entity} with {property}: {value} not found")
        {
        }
    }
}
