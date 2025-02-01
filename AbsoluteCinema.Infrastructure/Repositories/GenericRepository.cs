using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AbsoluteCinema.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _table = null!;

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

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!)
        {
            if (orderBy != null)
            {
                return await orderBy(_table.AsQueryable()).ToListAsync();
            }

            return await _table.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _table.FindAsync(id);
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

            //Шукає чи вже є локальна сутність, яку ми хочемо обновити
            var local = _table.Local.FirstOrDefault(e => _dbContext.Entry(e).Property("Id").CurrentValue.Equals(_dbContext.Entry(entity).Property("Id").CurrentValue));

            if (local != null)
            {
                //Оновлює дані локальної сутності
                _dbContext.Entry(local).CurrentValues.SetValues(entity);
            }
            else
            {
                //Отримує сутність
                _dbContext.Set<T>().Attach(entity);

                //Змінює дані сутності
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
