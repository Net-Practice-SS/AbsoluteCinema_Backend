﻿using AbsoluteCinema.Domain.Entities.Interfaces;

namespace AbsoluteCinema.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, IEntity;
        IGenreRepository GenreRepository {  get; }
        Task SaveChangesAsync();
    }
}
