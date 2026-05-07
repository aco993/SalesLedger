using SalesLedger.Application.DTOs;

namespace SalesLedger.Application.ViewModels;

public class ReportsViewModel
{
    public IReadOnlyList<SellerSalesDto> SellerPerformance { get; set; } = [];

    public IReadOnlyList<MonthlySalesDto> MonthlySales { get; set; } = [];

    public IReadOnlyList<ProductSalesDto> ProductPerformance { get; set; } = [];
}
