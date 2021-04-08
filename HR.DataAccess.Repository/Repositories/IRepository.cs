using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        Task Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
