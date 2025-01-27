using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IEntityStrategy<T> where T : IEntity
    {
        IQueryable<T> ApplyFilter(IQueryable<T> query);
    }
}
