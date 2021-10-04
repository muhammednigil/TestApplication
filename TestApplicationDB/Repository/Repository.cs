using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;
using TestApplicationInterface.Repository;

namespace TestApplicationDB.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Add(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;
            }
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
                DbSet.Remove(entity);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public async Task<T> GetById(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedDate = DateTime.UtcNow;
            }
            DbSet.Update(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includePathTypes)
        {
            var result = DbSet.Where(expression);
            if (skip.HasValue) result = result.Skip(skip.Value);
            if (take.HasValue) result = result.Take(take.Value);

            if(includePathTypes != null && includePathTypes.Length > 0)
            {
                foreach (var item in includePathTypes)
                {
                    result = result.Include(item);
                }
            }

            if(orderBy != null)
            {
                result = orderBy(result);
            }

            return result;
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includePathTypes)
        {
            return Task.Run(() => GetAll(expression, orderBy, skip, take, includePathTypes));
        }
    }
}
