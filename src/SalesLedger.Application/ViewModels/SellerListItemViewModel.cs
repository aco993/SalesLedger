namespace SalesLedger.Application.ViewModels;

public class SellerListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? CompanyName { get; set; }

    public string? ContactData { get; set; }

    public int TransactionsCount { get; set; }

    public decimal TotalSalesAmount { get; set; }
}
