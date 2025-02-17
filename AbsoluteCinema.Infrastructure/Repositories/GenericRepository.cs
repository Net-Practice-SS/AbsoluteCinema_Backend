using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using AbsoluteCinema.Domain.Exceptions;
using System.Linq.Expressions;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _table = null!;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public void Delete(int id)
        {
            T? existing = _table.Find(id);

            if (existing == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, "Id", id.ToString());
            }

            _table.Remove(existing);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            int page = 1,
            int pageSize = 6)
        {
            IQueryable<T> query = _table.AsQueryable();
            
            if (include != null)
            {
                query = include(query);
            }
            
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
           return await _table.FindAsync(id);
        }
        
        public IQueryable<T> GetWithStrategy(IEntityStrategy<T> filterStrategy, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
            int page = 1,
            int pageSize = 6)
        {
            var query = _table.AsQueryable();
            query = filterStrategy.ApplyFilter(query);

            if (orderBy != null)
            {
                orderBy(query);
            }
            
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(T entity)
        {
            var entityId = _dbContext.Entry(entity).Property("Id").CurrentValue;
            if (entityId == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, "Id", entityId?.ToString() ?? "null");
            }

            // Перевіряємо, чи існує сутність у базі
            bool exists = _table.Any(e => EF.Property<object>(e, "Id").Equals(entityId));
            if (!exists)
            {
                throw new EntityNotFoundException(typeof(T).Name, "Id", entityId?.ToString() ?? "null");
            }

            //Шукає чи вже є локальна сутність, яку ми хочемо обновити
            var local = _table.Local.FirstOrDefault(e => _dbContext.Entry(e).Property("Id").CurrentValue!.Equals(entityId));
            if (local != null)
            {
                //Оновлює дані локальної сутності
                _dbContext.Entry(local).CurrentValues.SetValues(entity);
            }
            else
            {
                //Отримує сутність
                _table.Attach(entity);

                //Змінює дані сутності
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
