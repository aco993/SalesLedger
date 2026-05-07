using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.DTOs;

public class RecentTransactionDto
{
    public int Id { get; set; }

    public DateTime TransactionDate { get; set; }

    public string SellerName { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string? BuyerName { get; set; }

    public decimal TotalAmount { get; set; }

    public PaymentType PaymentType { get; set; }
}
