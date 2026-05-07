using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Common;
using SalesLedger.Domain.Enums;

namespace SalesLedger.Domain.Entities;

public class Transaction : BaseEntity
{
    public DateTime TransactionDate { get; set; }

    public int SellerId { get; set; }

    public Seller Seller { get; set; } = null!;

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalAmount { get; set; }

    public PaymentType PaymentType { get; set; }

    public TransactionSourceType SourceType { get; set; }

    [MaxLength(120)]
    public string? BuyerName { get; set; }

    [MaxLength(1000)]
    public string? Notes { get; set; }
}
