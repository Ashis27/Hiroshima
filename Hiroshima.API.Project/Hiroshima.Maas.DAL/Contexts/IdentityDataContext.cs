using Hiroshima.Maas.DL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hiroshima.Maas.DAL.Contexts
{
    public class IdentityDataContext : IdentityDbContext<AppUser>
    {
        private IDbContextTransaction dbContextTransaction;
        public IdentityDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUserDB { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();

        }

        public new DbSet<T> Set<T>() where T : class

        {

            return base.Set<T>();

        }

        public void BeginTransaction()

        {

            dbContextTransaction = Database.BeginTransaction();

        }

        public void CommitTransaction()

        {

            if (dbContextTransaction != null)

            {

                dbContextTransaction.Commit();

            }

        }

        public void RollbackTransaction()

        {

            if (dbContextTransaction != null)

            {

                dbContextTransaction.Rollback();

            }

        }

        public void DisposeTransaction()

        {

            if (dbContextTransaction != null)

            {

                dbContextTransaction.Dispose();

            }

        }

    }
}
