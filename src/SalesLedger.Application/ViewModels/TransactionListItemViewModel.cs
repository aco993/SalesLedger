using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.ViewModels;

public class TransactionListItemViewModel
{
    public int Id { get; set; }

    public DateTime TransactionDate { get; set; }

    public string SellerName { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string CustomerDisplayName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalAmount { get; set; }

    public PaymentType PaymentType { get; set; }

    public TransactionSourceType SourceType { get; set; }
}
