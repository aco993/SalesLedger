using System.ComponentModel.DataAnnotations;
using SalesLedger.Domain.Common;
using SalesLedger.Domain.Enums;

namespace SalesLedger.Domain.Entities;

public class ImportLog : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    public DateTime ImportDate { get; set; }

    public int? SellerId { get; set; }

    public Seller? Seller { get; set; }

    public int ImportedRows { get; set; }

    public int FailedRows { get; set; }

    public ImportStatus Status { get; set; }

    [MaxLength(1000)]
    public string? ErrorMessage { get; set; }
}
