using Microsoft.AspNetCore.Mvc;

namespace SalesLedger.Web.Controllers;

public class AboutController : Controller
{
    public IActionResult Index() => View();
}
