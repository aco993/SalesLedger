using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ProductFormViewModel> GetCreateModelAsync();

    Task<ProductFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(ProductFormViewModel model, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(ProductFormViewModel model, CancellationToken cancellationToken = default);
}
