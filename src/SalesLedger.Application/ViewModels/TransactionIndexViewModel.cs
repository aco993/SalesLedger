using SalesLedger.Domain.Enums;

namespace SalesLedger.Application.ViewModels;

public class TransactionIndexViewModel
{
    public TransactionFilterViewModel Filter { get; set; } = new();

    public IReadOnlyList<TransactionListItemViewModel> Transactions { get; set; } = [];

    public IEnumerable<LookupItemViewModel> SellerOptions { get; set; } = [];

    public IEnumerable<LookupItemViewModel> ProductOptions { get; set; } = [];

    public IEnumerable<LookupItemViewModel> SourceTypeOptions { get; set; } = Enum.GetValues<TransactionSourceType>()
        .Select(x => new LookupItemViewModel { Value = x.ToString(), Text = x.ToString() });
}
