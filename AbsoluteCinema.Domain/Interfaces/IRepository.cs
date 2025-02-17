using AbsoluteCinema.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync( 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null, 
            int page = 1, 
            int pageSize = 6);
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        IQueryable<T> GetWithStrategy(
            IEntityStrategy<T> filterStrategy,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
            int page = 1,
            int pageSize = 6);
    }
}