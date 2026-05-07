using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;

namespace SalesLedger.Web.Controllers;

public class CustomersController(ICustomerService customerService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await customerService.GetAllAsync(cancellationToken));

    public async Task<IActionResult> Create()
        => View(await customerService.GetCreateModelAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await customerService.CreateAsync(model, cancellationToken);
        TempData["SuccessMessage"] = "Customer created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await customerService.GetEditModelAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CustomerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = await customerService.UpdateAsync(model, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Customer updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
