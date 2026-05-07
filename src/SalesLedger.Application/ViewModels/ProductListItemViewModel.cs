namespace SalesLedger.Application.ViewModels;

public class ProductListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

    public decimal? DefaultPrice { get; set; }

    public bool IsActive { get; set; }

    public int TransactionsCount { get; set; }
}
