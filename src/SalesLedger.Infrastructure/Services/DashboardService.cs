using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.DTOs;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class DashboardService(AppDbContext context) : IDashboardService
{
    public async Task<DashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var totalSalesAmount = await context.Transactions.SumAsync(x => (decimal?)x.TotalAmount, cancellationToken) ?? 0m;
        var totalTransactions = await context.Transactions.CountAsync(cancellationToken);
        var totalSellers = await context.Sellers.CountAsync(cancellationToken);
        var totalProducts = await context.Products.CountAsync(cancellationToken);

        var salesBySeller = await context.Transactions
            .GroupBy(x => x.Seller.Name)
            .Select(x => new SellerSalesDto
            {
                SellerName = x.Key,
                TotalAmount = x.Sum(t => t.TotalAmount),
                TransactionsCount = x.Count()
            })
            .OrderByDescending(x => x.TotalAmount)
            .ToListAsync(cancellationToken);

        var salesByMonthRaw = await context.Transactions
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

        var salesByMonth = salesByMonthRaw
            .Select(x => new MonthlySalesDto
            {
                Label = $"{x.Year}-{x.Month:D2}",
                TotalAmount = x.TotalAmount
            })
            .ToList();

        var topProducts = await context.Transactions
            .GroupBy(x => x.Product.Name)
            .Select(x => new ProductSalesDto
            {
                ProductName = x.Key,
                QuantitySold = x.Sum(t => t.Quantity),
                TotalAmount = x.Sum(t => t.TotalAmount)
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(5)
            .ToListAsync(cancellationToken);

        var recentTransactions = await context.Transactions
            .OrderByDescending(x => x.TransactionDate)
            .Take(8)
            .Select(x => new RecentTransactionDto
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                SellerName = x.Seller.Name,
                ProductName = x.Product.Name,
                BuyerName = x.Customer != null ? x.Customer.Name : x.BuyerName,
                TotalAmount = x.TotalAmount,
                PaymentType = x.PaymentType
            })
            .ToListAsync(cancellationToken);

        return new DashboardViewModel
        {
            TotalSalesAmount = totalSalesAmount,
            TotalTransactions = totalTransactions,
            TotalSellers = totalSellers,
            TotalProducts = totalProducts,
            SalesBySeller = salesBySeller,
            SalesByMonth = salesByMonth,
            TopProducts = topProducts,
            RecentTransactions = recentTransactions
        };
    }
}
