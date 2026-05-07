using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionIndexViewModel> GetIndexViewModelAsync(TransactionFilterViewModel filter, CancellationToken cancellationToken = default);

    Task<TransactionFormViewModel> GetCreateModelAsync(CancellationToken cancellationToken = default);

    Task<TransactionFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default);

    Task<TransactionListItemViewModel?> GetDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(TransactionFormViewModel model, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TransactionFormViewModel model, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
