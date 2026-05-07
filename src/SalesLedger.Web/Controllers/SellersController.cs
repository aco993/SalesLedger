using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;

namespace SalesLedger.Web.Controllers;

public class SellersController(ISellerService sellerService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await sellerService.GetAllAsync(cancellationToken));

    public async Task<IActionResult> Create()
        => View(await sellerService.GetCreateModelAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SellerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await sellerService.CreateAsync(model, cancellationToken);
        TempData["SuccessMessage"] = "Seller created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await sellerService.GetEditModelAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SellerFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = await sellerService.UpdateAsync(model, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Seller updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
