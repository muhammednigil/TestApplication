using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.Entities;

namespace TestApplicationInterface.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task<T> GetById(object id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includePathTypes);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params string[] includePathTypes);
    }
}
