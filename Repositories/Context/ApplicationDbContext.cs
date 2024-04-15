using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.Models;
using Repositories.Context.Seed;
using Repositories.FluentConfig;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options): base (options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<LoginEvent> LoginEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfig(modelBuilder.Entity<User>());
            new RolConfig(modelBuilder.Entity<Rol>());
            new LoginEventConfig(modelBuilder.Entity<LoginEvent>());

            #region Seed

            new RolSeedConfig(modelBuilder.Entity<Rol>());
            #endregion
        }

        public void Commit()
        {
            try
            {
                using var transactionScope = new TransactionScope();
                SaveChanges();
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                //La transaccion fluye a travesde las continuas llamadas asincronas
                using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                //Hace cambios sobre las modificaciones realizadas en los datos de los modelos
                await SaveChangesAsync();
                //Completa la transacción
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Si ocurre algun error en el "Save changes" intenta volver a ejecutar la modificacion, pero si hay error, entonces no ejecuta ninguna cambio sobre la bd
                ex.Entries.Single().Reload();
            }
        }

        public DbContext GetContext()
        {
            return this;
        }

        public DbSet<TEntity> GetSet<TId, TEntity>()
            where TId : struct
            where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}
