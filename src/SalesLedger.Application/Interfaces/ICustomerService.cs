using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<CustomerFormViewModel> GetCreateModelAsync();

    Task<CustomerFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(CustomerFormViewModel model, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(CustomerFormViewModel model, CancellationToken cancellationToken = default);
}
