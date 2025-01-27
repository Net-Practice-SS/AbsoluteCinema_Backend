using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
        T? GetById(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        IQueryable<T> GetWithStrategy(
            IEntityStrategy<T> filterStrategy,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
    }
}
