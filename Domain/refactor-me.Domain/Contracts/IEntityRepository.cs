using refactor_me.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Domain.Contracts
{
    //Base Repository Interface
    public interface IEntityRepository<TEntity>
    where TEntity : DataEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        void AddAsync(TEntity entity);

        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        Task EditAsync(TEntity entity);

        void Edit(TEntity entity);

        bool Any(Expression<Func<TEntity, bool>> where);

        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        void Delete(Guid id);

        void DeleteAsync(Guid id);

    }
}
