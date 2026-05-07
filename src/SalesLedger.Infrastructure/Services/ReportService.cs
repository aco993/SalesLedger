using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.DTOs;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class ReportService(AppDbContext context) : IReportService
{
    public async Task<ReportsViewModel> GetReportsAsync(CancellationToken cancellationToken = default)
    {
        var sellerPerformance = await context.Transactions
            .GroupBy(x => x.Seller.Name)
            .Select(x => new SellerSalesDto
            {
                SellerName = x.Key,
                TransactionsCount = x.Count(),
                TotalAmount = x.Sum(t => t.TotalAmount)
            })
            .OrderByDescending(x => x.TotalAmount)
            .ToListAsync(cancellationToken);

        var monthlySalesRaw = await context.Transactions
            .GroupBy(x => new { x.TransactionDate.Year, x.TransactionDate.Month })
            .Select(x => new
            {
                x.Key.Year,
                x.Key.Month,
                TotalAmount = x.Sum(t => t.TotalAmount)
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToListAsync(cancellationToken);

        var monthlySales = monthlySalesRaw
            .Select(x => new MonthlySalesDto
            {
                Label = $"{x.Year}-{x.Month:D2}",
                TotalAmount = x.TotalAmount
            })
            .ToList();

        var productPerformance = await context.Transactions
            .GroupBy(x => x.Product.Name)
            .Select(x => new ProductSalesDto
            {
                ProductName = x.Key,
                QuantitySold = x.Sum(t => t.Quantity),
                TotalAmount = x.Sum(t => t.TotalAmount)
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(10)
            .ToListAsync(cancellationToken);

        return new ReportsViewModel
        {
            SellerPerformance = sellerPerformance,
            MonthlySales = monthlySales,
            ProductPerformance = productPerformance
        };
    }
}
