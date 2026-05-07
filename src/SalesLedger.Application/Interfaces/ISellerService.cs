using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface ISellerService
{
    Task<IReadOnlyList<SellerListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<SellerFormViewModel> GetCreateModelAsync();

    Task<SellerFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(SellerFormViewModel model, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(SellerFormViewModel model, CancellationToken cancellationToken = default);
}
