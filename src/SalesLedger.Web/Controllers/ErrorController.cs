using Microsoft.AspNetCore.Mvc;
using SalesLedger.Web.Models;

namespace SalesLedger.Web.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    public IActionResult Index()
        => View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
}
