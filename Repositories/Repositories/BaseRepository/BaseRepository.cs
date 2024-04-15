using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repositories.IRepositories;
using Repositories.IRepositories.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.BaseRepository
{
    public class BaseRepository<TId, TEntity> : IRepositories.IBaseRepository.IBaseReporitory<TId, TEntity>
        where TId : struct
        where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;


        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await _unitOfWork.GetSet<TId, TEntity>().AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<TEntity> FindByIdAsync(TId id)
            => await _unitOfWork.GetSet<TId, TEntity>().FindAsync(id) ?? null;


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, string includeProperties = "")
                    => await BuildQuery(filter, orderBy, include, includeProperties).ToListAsync();

        private IQueryable<TEntity> BuildQuery(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            string includeProperties = "")
        {
            var query = _unitOfWork.GetSet<TId, TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(property =>
            {
                query = query.Include(property);
            });

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
        public async Task UpdateAsync(TEntity entity)
        {
            ValidateEntity(entity);
            _unitOfWork.GetSet<TId, TEntity>().Update(entity);
            await _unitOfWork.CommitAsync();

        }
        private static void ValidateEntity(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El objeto entidad no puede ser nulo");
            }
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, TEntity>> selector = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _unitOfWork.GetSet<TId, TEntity>().AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(property =>
            {
                query = query.Include(property);
            });

            if (orderBy != null)
            {
                if (selector != null)
                {
                    return await orderBy(query).Select(selector).FirstOrDefaultAsync();
                }

                return await orderBy(query).FirstOrDefaultAsync();
            }

            if (selector != null)
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
