using SalesLedger.Application.DTOs;

namespace SalesLedger.Application.ViewModels;

public class DashboardViewModel
{
    public decimal TotalSalesAmount { get; set; }

    public int TotalTransactions { get; set; }

    public int TotalSellers { get; set; }

    public int TotalProducts { get; set; }

    public IReadOnlyList<SellerSalesDto> SalesBySeller { get; set; } = [];

    public IReadOnlyList<MonthlySalesDto> SalesByMonth { get; set; } = [];

    public IReadOnlyList<ProductSalesDto> TopProducts { get; set; } = [];

    public IReadOnlyList<RecentTransactionDto> RecentTransactions { get; set; } = [];
}
