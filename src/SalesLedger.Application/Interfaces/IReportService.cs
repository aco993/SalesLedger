using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface IReportService
{
    Task<ReportsViewModel> GetReportsAsync(CancellationToken cancellationToken = default);
}
