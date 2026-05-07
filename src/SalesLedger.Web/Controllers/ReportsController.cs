using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;

namespace SalesLedger.Web.Controllers;

public class ReportsController(IReportService reportService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await reportService.GetReportsAsync(cancellationToken));
}
