using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReactiveUIApplication.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(DbContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetEntityList()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetEntityList(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity GetEntityById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity item)
        {
            _dbSet.Add(item);
            Context.SaveChanges();
        }

        public abstract void Update(TEntity item);

        public virtual void Remove(TEntity item)
        {
            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }
    }
}