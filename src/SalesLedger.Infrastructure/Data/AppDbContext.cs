using Microsoft.EntityFrameworkCore;
using SalesLedger.Domain.Entities;

namespace SalesLedger.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Seller> Sellers => Set<Seller>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<ImportLog> ImportLogs => Set<ImportLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(x => x.Entity is Domain.Common.BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (Domain.Common.BaseEntity)entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedOnUtc = DateTime.UtcNow;
            }
            else
            {
                entity.ModifiedOnUtc = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
