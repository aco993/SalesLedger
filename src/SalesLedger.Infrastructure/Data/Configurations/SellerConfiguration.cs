using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesLedger.Domain.Entities;

namespace SalesLedger.Infrastructure.Data.Configurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(120);
        builder.Property(x => x.CompanyName).HasMaxLength(160);
        builder.Property(x => x.ContactData).HasMaxLength(160);
        builder.Property(x => x.Notes).HasMaxLength(600);
    }
}
