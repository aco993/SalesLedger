using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default);
}
