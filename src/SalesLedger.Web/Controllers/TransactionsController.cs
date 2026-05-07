using Microsoft.AspNetCore.Mvc;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;

namespace SalesLedger.Web.Controllers;

public class TransactionsController(ITransactionService transactionService) : Controller
{
    public async Task<IActionResult> Index([FromQuery] TransactionFilterViewModel filter, CancellationToken cancellationToken)
    {
        var model = await transactionService.GetIndexViewModelAsync(filter, cancellationToken);
        return View(model);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = await transactionService.GetCreateModelAsync(cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransactionFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var hydratedModel = await transactionService.GetCreateModelAsync(cancellationToken);
            model.SellerOptions = hydratedModel.SellerOptions;
            model.ProductOptions = hydratedModel.ProductOptions;
            model.CustomerOptions = hydratedModel.CustomerOptions;
            return View(model);
        }

        await transactionService.CreateAsync(model, cancellationToken);
        TempData["SuccessMessage"] = "Transaction created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await transactionService.GetEditModelAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TransactionFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var hydratedModel = model.Id.HasValue
                ? await transactionService.GetEditModelAsync(model.Id.Value, cancellationToken)
                : await transactionService.GetCreateModelAsync(cancellationToken);

            if (hydratedModel is not null)
            {
                model.SellerOptions = hydratedModel.SellerOptions;
                model.ProductOptions = hydratedModel.ProductOptions;
                model.CustomerOptions = hydratedModel.CustomerOptions;
            }

            return View(model);
        }

        var updated = await transactionService.UpdateAsync(model, cancellationToken);
        if (!updated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Transaction updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var model = await transactionService.GetDetailsAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await transactionService.DeleteAsync(id, cancellationToken);
        TempData[deleted ? "SuccessMessage" : "ErrorMessage"] = deleted
            ? "Transaction deleted successfully."
            : "Transaction could not be deleted.";

        return RedirectToAction(nameof(Index));
    }
}
