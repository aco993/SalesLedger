using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesLedger.Domain.Entities;

namespace SalesLedger.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(160);
        builder.Property(x => x.Category).HasMaxLength(100);
        builder.Property(x => x.DefaultPrice).HasColumnType("decimal(18,2)");
    }
}
