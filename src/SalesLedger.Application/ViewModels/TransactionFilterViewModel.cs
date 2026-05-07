using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.ViewModels;

public class TransactionFilterViewModel
{
    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int? SellerId { get; set; }

    public int? ProductId { get; set; }

    public TransactionSourceType? SourceType { get; set; }

    public string? SearchTerm { get; set; }
}
