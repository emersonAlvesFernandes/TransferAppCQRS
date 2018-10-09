using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly TransferAppContext _context;

        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(TransferAppContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj) => DbSet.Add(obj);

        public TEntity GetById(Guid id) => DbSet.Find(id);

        public IQueryable<TEntity> GetAll() => DbSet;

        public void Update(TEntity obj) => DbSet.Update(obj);

        public void Remove(Guid id) => DbSet.Remove(DbSet.Find(id));

        public int SaveChanges() => _context.SaveChanges();
            
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
