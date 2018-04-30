using refactor_me.Domain.Contracts;
using refactor_me.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using refactor_me.Infrastructure.Extentions;

namespace refactor_me.Domain.BaseRepository
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity>
         where TEntity : DataEntity

    {

        protected EntityRepository(
            IContextFactory contextFactory)
        {
            // Verify parameters.
            "contextFactory".IsNotNullArgument(contextFactory);

            this._ContextFactory = contextFactory;

        }



        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var response = Enumerable.Empty<TEntity>();

            using (DbContext context = await this.ConnectAsync().ConfigureAwait(false))
            {
                // Create relevant object set.
                var query =
                    context
                        .Set<TEntity>()
                        .AsQueryable();


                // Add includes.
                includes
                    .ForEachElement(
                        include =>
                            query = query.Include(include));

                // Get results.
                response = await query.ToListAsync();
            }

            return response;
        }

        public void Add(TEntity entity)
        {
            // Verify parameters.
            "entity".IsNotNullArgument(entity);

            // Add accordingly.
            using (DbContext context = this.Connect())
            {

                // Add accordingly.                                
                context.Set<TEntity>().Add(entity);


                // Save.
                try
                {
                    context.SaveChanges();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public async void AddAsync(TEntity entity)
        {
            // Verify parameters.
            "entity".IsNotNullArgument(entity);

            // Add accordingly.
            using (DbContext context = await this.ConnectAsync().ConfigureAwait(false))
            {

                // Add accordingly.                                
                context.Set<TEntity>().Add(entity);


                // Save.
                try
                {
                    await context.SaveChangesAsync();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            // Verify parameters.
            "entity".IsNotNullArgument(entities);


            using (DbContext context = this.Connect())
            {
                entities.ForEachElement(
                    entity =>
                    {
                        // Add accordingly.
                        context.Set<TEntity>().Add(entity);
                    });

                // Save.
                context.SaveChanges();
            }
        }


        public async Task EditAsync(TEntity entity)
        {
            "entity".IsNotNullArgument(entity);

            // Connect.
            using (var context = await this.ConnectAsync().ConfigureAwait(false))
            {
                // Retrieve the existing one.
                TEntity original = this.GetEntity(context, entity.Id);

                // Forward accordingly.
                this.DataTransfer(original, entity);

                // Save changes.
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public void Edit(TEntity entity)
        {
            "entity".IsNotNullArgument(entity);

            // Connect.
            using (var context = this.Connect())
            {
                // Retrieve the existing one.
                TEntity original = this.GetEntity(context, entity.Id);

                // Forward accordingly.
                this.DataTransfer(original, entity);

                // Save changes.
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            // Connect
            using (DbContext context = this.Connect())
            {
                // Retrieve entity.
                TEntity entity = this.GetEntity(context, id);

                if (entity != null)
                {
                    // Delete accordingly.
                    context.Set<TEntity>().Remove(entity);
                    context.SaveChanges();
                }
            }
        }

        public async void DeleteAsync(Guid id)
        {
            // Connect
            using (DbContext context = await this.ConnectAsync().ConfigureAwait(false))
            {
                // Retrieve entity.
                TEntity entity = this.GetEntity(context, id);

                if (entity != null)
                {
                    // Delete accordingly.
                    context.Set<TEntity>().Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected virtual void DataTransfer(TEntity original, TEntity modified)
        {
            // Verify parameters.
            "original".IsNotNullArgument(original);
            "modified".IsNotNullArgument(modified);
        }

        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            var response = false;

            // Connect
            using (DbContext context = this.Connect())
            {
                // Retrieve entity.
                response =
                    context.Set<TEntity>()
                        .Any(where);
            }

            return response;
        }

        public async Task<TEntity> GetEntityAsync(
                    Expression<Func<TEntity, bool>> where,
                    params Expression<Func<TEntity, object>>[] includes)
        {
            // Resulting instance.
            TEntity entity = null;

            using (DbContext context = await this.ConnectAsync().ConfigureAwait(false))
            {
                // Create relevant object set.
                var query =
                    context
                        .Set<TEntity>()
                        .Where(where);

                // Add includes.
                includes
                    .ForEachElement(
                        include =>
                            query = query.Include(include));

                entity = await
                    query
                        .FirstOrDefaultAsync<TEntity>().ConfigureAwait(false);
            }

            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetEntitiesAsync(
             Expression<Func<TEntity, bool>> where,
             params Expression<Func<TEntity, object>>[] includes)
        {
            // Resulting collection.
            IEnumerable<TEntity> entities = Enumerable.Empty<TEntity>();

            using (DbContext context = await this.ConnectAsync().ConfigureAwait(false))
            {
                // Create relevant object set.
                var query =
                    context
                        .Set<TEntity>()
                        .Where(where);

                // Add includes.
                includes
                    .ForEachElement(
                        include =>
                            query = query.Include(include));

                // Get results.
                entities =
                    await query.ToListAsync().ConfigureAwait(false);
            }

            // Return resulting collection.
            return entities;
        }



        #region InstanceMethodes
        protected async Task<DbContext> ConnectAsync()
        {
            return await this._ContextFactory.ConnectAsync();
        }

        protected DbContext Connect()
        {
            return this._ContextFactory.Connect();

        }

        protected TEntity GetEntity(
           DbContext context,
           Guid id,
           params Expression<Func<TEntity, object>>[] includes)
        {
            // Resulting instance.
            TEntity entity = default(TEntity);

            // Create relevant object set.
            var query =
                context
                    .Set<TEntity>()
                    .Where(
                        obj =>
                            obj.Id == id);

            // Add includes.
            includes
                .ForEachElement(
                    include =>
                        query = query.Include(include));

            entity =
                query
                    .FirstOrDefault<TEntity>();

            return entity;
        }

        protected async Task<TEntity> GetEntityAsync(
            DbContext context,
            Guid id,
            params Expression<Func<TEntity, object>>[] includes)
        {

            // Create relevant object set.
            var query =
                context
                    .Set<TEntity>()
                    .Where(
                        obj =>
                            obj.Id == id);

            // Add includes.
            includes
                .ForEachElement(
                    include =>
                        query = query.Include(include));

            return await query.FirstOrDefaultAsync<TEntity>().ConfigureAwait(false);
        }

        protected TEntity GetEntityById(
            DbContext context,
            Guid id,
            params Expression<Func<TEntity, object>>[] includes)
        {
            // Resulting instance.
            TEntity entity = default(TEntity);

            // Create relevant object set.

            var query =
                context.Set<TEntity>()
                    .Where(
                        obj =>
                            obj.Id == id);

            // Add includes.
            includes
                .ForEachElement(
                    include =>
                        query = query.Include(include));

            entity =
                query
                    .FirstOrDefault<TEntity>();


            return entity;
        }

        #endregion


        private readonly IContextFactory _ContextFactory;
        private readonly string _ConnectionString;
    }
}
