using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        DbContext GetContext();
        DbSet<TEntity> GetSet<TId, TEntity>()
            where TId : struct
            where TEntity : class;
    }
}
