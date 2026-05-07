using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Common;

namespace SalesLedger.Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    [MaxLength(160)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Category { get; set; }

    public decimal? DefaultPrice { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
