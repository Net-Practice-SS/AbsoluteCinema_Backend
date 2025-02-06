namespace AbsoluteCinema.Domain.Exceptions
{
    public class AlreadyExistEntityException : Exception
    {
        public AlreadyExistEntityException(string entity, string id) : base($"{entity} with id: {id} already exist")
        {
        }
    }
}
