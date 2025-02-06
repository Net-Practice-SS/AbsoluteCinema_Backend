using AbsoluteCinema.Domain.Entities;
using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Repositories;

namespace AbsoluteCinema.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();
        private IGenreRepository? _genreRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new GenericRepository<T>(_dbContext);
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }

        public IGenreRepository GenreRepository
        {
            get
            {
                if (_genreRepository == null)
                {
                    _genreRepository = new GenreRepository(_dbContext);
                }
                return _genreRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
