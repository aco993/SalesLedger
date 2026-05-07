using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;

namespace SalesLedger.Web.Controllers;

public class DashboardController(IDashboardService dashboardService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await dashboardService.GetDashboardAsync(cancellationToken);
        return View(model);
    }
}
