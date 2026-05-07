using SalesLedger.Application.ViewModels;

namespace SalesLedger.Application.Interfaces;

public interface IImportService
{
    Task<IReadOnlyList<ImportLogListItemViewModel>> GetImportLogsAsync(CancellationToken cancellationToken = default);
}
