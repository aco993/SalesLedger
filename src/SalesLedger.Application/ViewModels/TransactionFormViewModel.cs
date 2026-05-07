using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.ViewModels;

public class TransactionFormViewModel
{
    public int? Id { get; set; }

    [Display(Name = "Transaction Date")]
    [DataType(DataType.Date)]
    public DateTime TransactionDate { get; set; } = DateTime.Today;

    [Required]
    [Display(Name = "Seller")]
    public int SellerId { get; set; }

    [Display(Name = "Customer")]
    public int? CustomerId { get; set; }

    [StringLength(120)]
    [Display(Name = "Buyer Name")]
    public string? BuyerName { get; set; }

    [Required]
    [Display(Name = "Product")]
    public int ProductId { get; set; }

    [Range(1, 100000)]
    public int Quantity { get; set; } = 1;

    [Range(typeof(decimal), "0.01", "999999999")]
    [Display(Name = "Unit Price")]
    public decimal UnitPrice { get; set; }

    [Range(typeof(decimal), "0.01", "999999999")]
    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "Payment Type")]
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;

    [Display(Name = "Source Type")]
    public TransactionSourceType SourceType { get; set; } = TransactionSourceType.Manual;

    [StringLength(1000)]
    public string? Notes { get; set; }

    public IEnumerable<LookupItemViewModel> SellerOptions { get; set; } = [];

    public IEnumerable<LookupItemViewModel> ProductOptions { get; set; } = [];

    public IEnumerable<LookupItemViewModel> CustomerOptions { get; set; } = [];
}
