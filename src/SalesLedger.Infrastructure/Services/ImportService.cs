using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class ImportService(AppDbContext context) : IImportService
{
    public async Task<IReadOnlyList<ImportLogListItemViewModel>> GetImportLogsAsync(CancellationToken cancellationToken = default)
    {
        return await context.ImportLogs
            .AsNoTracking()
            .OrderByDescending(x => x.ImportDate)
            .Select(x => new ImportLogListItemViewModel
            {
                Id = x.Id,
                FileName = x.FileName,
                ImportDate = x.ImportDate,
                SellerName = x.Seller != null ? x.Seller.Name : null,
                ImportedRows = x.ImportedRows,
                FailedRows = x.FailedRows,
                Status = x.Status,
                ErrorMessage = x.ErrorMessage
            })
            .ToListAsync(cancellationToken);
    }
}
