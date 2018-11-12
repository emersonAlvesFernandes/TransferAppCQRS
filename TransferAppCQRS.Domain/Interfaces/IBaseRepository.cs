using System;
using System.Collections.Generic;
using System.Linq;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }

    public interface IBaseWriteRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        int SaveChanges();
    }

    public interface IBaseReadRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        List<TEntity> GetAll();
    }
}
