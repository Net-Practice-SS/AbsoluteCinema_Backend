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
        private IMovieRepository? _movieRepository;
        private IActorRepository? _actorRepository;

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
        
        public IActorRepository ActorRepository
        {
            get
            {
                if (_actorRepository == null)
                {
                    _actorRepository = new ActorRepository(_dbContext);
                }
                return _actorRepository;
            }
        }

        public IMovieRepository MovieRepository
        {
            get
            {
                if (_movieRepository == null)
                {
                    _movieRepository = new MovieRepository(_dbContext);
                }
                return _movieRepository;
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
