using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _table = null!;

        public GenericRepository()
        {
            _dbContext = new AppDbContext();
            _table = _dbContext.Set<T>();
        }

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                return;
            }

            _table.Add(entity);
        }

        public void Delete(int id)
        {
            T? existing = _table.Find(id);

            if (existing == null)
            {
                return;
            }

            _table.Remove(existing);
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!)
        {
            if (orderBy != null)
            {
                return orderBy(_table.AsQueryable()).ToList();
            }

            return _table.ToList();
        }

        public T? GetById(int id)
        {
            return _table.Find(id);
        }

        public IQueryable<T> GetWithStrategy(IEntityStrategy<T> filterStrategy, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!)
        {
            var query = _table.AsQueryable();
            query = filterStrategy.ApplyFilter(query);

            if (orderBy != null)
            {
                orderBy(query);
            }

            return query;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                return;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
