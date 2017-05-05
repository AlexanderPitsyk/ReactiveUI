using System.Collections.Generic;

namespace ReactiveUIApplication.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity item);

        TEntity GetEntityById(int id);

        IEnumerable<TEntity> GetEntityList();

        void Remove(TEntity item);

        void Update(TEntity item);
    }
}