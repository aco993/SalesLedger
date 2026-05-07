using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;

namespace SalesLedger.Web.Controllers;

public class ProductsController(IProductService productService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await productService.GetAllAsync(cancellationToken));

    public async Task<IActionResult> Create()
        => View(await productService.GetCreateModelAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await productService.CreateAsync(model, cancellationToken);
        TempData["SuccessMessage"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await productService.GetEditModelAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var updated = await productService.UpdateAsync(model, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Product updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
