using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class EfRepository<TContext> : IEfRepository where TContext : DbContext
    {
        protected TContext Context;

        public EfRepository(TContext context)
        {
            Context = context;
        }

        public void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity
            => Context.Set<TEntity>().Add(entity);

        public void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<TEntity>(object id)
            where TEntity : class, IEntity
            => Delete(Context.Set<TEntity>().Find(id));

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void Save() => Context.SaveChanges();

        public Task SaveAsync() => Context.SaveChangesAsync();

        protected IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            includeProperties ??= string.Empty;
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','},
                    StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.AsQueryable();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
            => GetQueryable(null, orderBy, includeProperties, skip, take).ToList();

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
            => await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();

        public IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
            => GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
            => await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();

        public TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class, IEntity
            => GetQueryable(filter, null, includeProperties).SingleOrDefault();

        public async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class, IEntity
            => await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();

        public TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
            where TEntity : class, IEntity
            => GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();

        public async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class, IEntity
            => await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();

        public TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity
            => Context.Set<TEntity>().Find(id);

        public async Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity
            => await Context.Set<TEntity>().FindAsync(id);

        public int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
            => GetQueryable(filter).Count();

        public Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
            => GetQueryable(filter).CountAsync();

        public bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
            => GetQueryable(filter).Any();

        public Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
            => GetQueryable(filter).AnyAsync();
    }
}