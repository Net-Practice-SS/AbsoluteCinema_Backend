namespace AbsoluteCinema.Domain.Exceptions
{
    public class AlreadyExistEntityException : Exception
    {
        public AlreadyExistEntityException(string entity, string property, string id) : base($"{entity} with {property}: {id} already exists...")
        {
        }
    }
}
