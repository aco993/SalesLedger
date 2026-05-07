using Microsoft.EntityFrameworkCore;
using SalesLedger.Domain.Entities;
using SalesLedger.Domain.Enums;

namespace SalesLedger.Infrastructure.Data.Seed;

public static class AppDbInitializer
{
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Sellers.AnyAsync(cancellationToken))
        {
            return;
        }

        var sellers = new List<Seller>
        {
            new() { Name = "Nikola Jovanovic", CompanyName = "TNT Devices", ContactData = "nikola@tnt-devices.local", Notes = "Core marketplace seller with mixed historical records." },
            new() { Name = "Semir Halilovic", CompanyName = "SH Mobile Trade", ContactData = "semir@example.local", Notes = "Recurring external seller partner." },
            new() { Name = "Daniel Weber", CompanyName = "Weber Resale", ContactData = "+49 30 555 0190", Notes = "German reseller contact for test portfolio data." },
            new() { Name = "Amina Kovac", CompanyName = "AK Accessories", ContactData = "amina@example.local", Notes = "Accessory-focused seller with fast turnover." }
        };

        var products = new List<Product>
        {
            new() { Name = "Lenovo ThinkPad T480", Category = "Laptop", DefaultPrice = 349.00m, IsActive = true },
            new() { Name = "Dell Latitude 7490", Category = "Laptop", DefaultPrice = 379.00m, IsActive = true },
            new() { Name = "HP EliteBook 840 G5", Category = "Laptop", DefaultPrice = 329.00m, IsActive = true },
            new() { Name = "Samsung Galaxy A52", Category = "Smartphone", DefaultPrice = 189.00m, IsActive = true },
            new() { Name = "iPhone 11 64GB", Category = "Smartphone", DefaultPrice = 339.00m, IsActive = true },
            new() { Name = "USB-C Docking Station", Category = "Accessory", DefaultPrice = 69.00m, IsActive = true },
            new() { Name = "Laptop Charger 65W", Category = "Accessory", DefaultPrice = 24.90m, IsActive = true },
            new() { Name = "27 inch IPS Monitor", Category = "Monitor", DefaultPrice = 149.00m, IsActive = true }
        };

        var customers = new List<Customer>
        {
            new() { Name = "Maja Petrovic", ContactData = "maja@example.local", City = "Subotica", Notes = "Repeat private buyer." },
            new() { Name = "Oliver Schmidt", ContactData = "oliver.schmidt@example.local", City = "Berlin", Notes = "B2B small-office contact." },
            new() { Name = "Ivana Markovic", ContactData = "ivana@example.local", City = "Novi Sad", Notes = "Often buys refurbished phones." },
            new() { Name = "Hasan Basic", ContactData = "+387 61 555 120", City = "Tuzla", Notes = "Cross-border buyer." },
            new() { Name = "Green Byte Studio", ContactData = "procurement@greenbyte.local", City = "Hamburg", Notes = "Startup office equipment purchases." }
        };

        await context.Sellers.AddRangeAsync(sellers, cancellationToken);
        await context.Products.AddRangeAsync(products, cancellationToken);
        await context.Customers.AddRangeAsync(customers, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var baseDate = new DateTime(2025, 10, 1);
        var transactions = new List<Transaction>();
        var random = new Random(42);

        for (var month = 0; month < 7; month++)
        {
            for (var i = 0; i < 6; i++)
            {
                var seller = sellers[(month + i) % sellers.Count];
                var product = products[(month * 2 + i) % products.Count];
                var customer = i % 5 == 0 ? null : customers[(month + i) % customers.Count];
                var quantity = random.Next(1, 5);
                var unitPrice = product.DefaultPrice ?? random.Next(40, 420);

                transactions.Add(new Transaction
                {
                    TransactionDate = baseDate.AddMonths(month).AddDays(i * 4),
                    SellerId = seller.Id,
                    ProductId = product.Id,
                    CustomerId = customer?.Id,
                    BuyerName = customer?.Name ?? $"Walk-in buyer {month + 1}-{i + 1}",
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    TotalAmount = unitPrice * quantity,
                    PaymentType = (PaymentType)((i % 4) + 1),
                    SourceType = (month % 3) switch
                    {
                        0 => TransactionSourceType.ExcelImport,
                        1 => TransactionSourceType.Manual,
                        _ => TransactionSourceType.PaperRecord
                    },
                    Notes = i % 2 == 0 ? "Verified and matched against source records." : "Prepared for later BI consolidation."
                });
            }
        }

        var importLogs = new List<ImportLog>
        {
            new() { FileName = "TNT.xlsx", ImportDate = DateTime.UtcNow.AddDays(-12), SellerId = sellers[0].Id, ImportedRows = 148, FailedRows = 3, Status = ImportStatus.CompletedWithWarnings, ErrorMessage = "3 rows skipped because product matching was ambiguous." },
            new() { FileName = "Transakcije.xlsx", ImportDate = DateTime.UtcNow.AddDays(-30), SellerId = sellers[1].Id, ImportedRows = 96, FailedRows = 0, Status = ImportStatus.Completed },
            new() { FileName = "Handwritten_Q4_scan_batch_01.pdf", ImportDate = DateTime.UtcNow.AddDays(-4), SellerId = sellers[3].Id, ImportedRows = 0, FailedRows = 0, Status = ImportStatus.Pending, ErrorMessage = "OCR module not implemented yet. Placeholder log created for future processing." }
        };

        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        await context.ImportLogs.AddRangeAsync(importLogs, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
