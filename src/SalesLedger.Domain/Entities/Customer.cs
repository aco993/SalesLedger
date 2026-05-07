using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Common;

namespace SalesLedger.Domain.Entities;

public class Customer : BaseEntity
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? ContactData { get; set; }

    [MaxLength(120)]
    public string? City { get; set; }

    [MaxLength(400)]
    public string? Notes { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
