using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
        Task<T> GetById(int id);
        IQueryable<T> GetWithStrategy(
            IEntityStrategy<T> filterStrategy, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
