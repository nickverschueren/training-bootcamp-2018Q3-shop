using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Data.Model.Mapping;

namespace Shop.Api.Data.Model
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new BasketMapping());
            modelBuilder.ApplyConfiguration(new BasketItemMapping());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AuditEntities();
            return base.SaveChanges();
        }

        private void AuditEntities()
        {
            var now = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
            {
                var auditable = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    auditable.LastUpdated = now;
                }
                if (entry.State == EntityState.Added)
                {
                    auditable.Created = now;
                }
            }
        }
    }
}
