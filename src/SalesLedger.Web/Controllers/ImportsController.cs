using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;

namespace SalesLedger.Web.Controllers;

public class ImportsController(IImportService importService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await importService.GetImportLogsAsync(cancellationToken));
}
