using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Common;

namespace SalesLedger.Domain.Entities;

public class Seller : BaseEntity
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(160)]
    public string? CompanyName { get; set; }

    [MaxLength(160)]
    public string? ContactData { get; set; }

    [MaxLength(600)]
    public string? Notes { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
