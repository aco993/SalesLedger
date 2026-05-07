using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.ViewModels;

public class ImportLogListItemViewModel
{
    public int Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public DateTime ImportDate { get; set; }

    public string? SellerName { get; set; }

    public int ImportedRows { get; set; }

    public int FailedRows { get; set; }

    public ImportStatus Status { get; set; }

    public string? ErrorMessage { get; set; }
}
