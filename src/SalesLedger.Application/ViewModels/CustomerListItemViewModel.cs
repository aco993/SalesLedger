namespace SalesLedger.Application.ViewModels;

public class CustomerListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ContactData { get; set; }

    public string? City { get; set; }

    public int TransactionsCount { get; set; }
}
