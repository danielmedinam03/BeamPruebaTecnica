using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories.IBaseRepository
{
    public interface IBaseReporitory<TId, TEntity> : IDisposable
        where TId : struct
        where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, string includeProperties = "");
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, string includeProperties = "");

    }
}
