namespace SalesLedger.Application.DTOs;

public class SellerSalesDto
{
    public string SellerName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public int TransactionsCount { get; set; }
}
