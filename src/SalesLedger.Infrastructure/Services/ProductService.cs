using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Domain.Entities;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class ProductService(AppDbContext context) : IProductService
{
    public Task<ProductFormViewModel> GetCreateModelAsync() => Task.FromResult(new ProductFormViewModel { IsActive = true });

    public async Task<IReadOnlyList<ProductListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new ProductListItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                DefaultPrice = x.DefaultPrice,
                IsActive = x.IsActive,
                TransactionsCount = x.Transactions.Count
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ProductFormViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                DefaultPrice = x.DefaultPrice,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CreateAsync(ProductFormViewModel model, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(new Product
        {
            Name = model.Name,
            Category = model.Category,
            DefaultPrice = model.DefaultPrice,
            IsActive = model.IsActive
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(ProductFormViewModel model, CancellationToken cancellationToken = default)
    {
        if (!model.Id.HasValue)
        {
            return false;
        }

        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == model.Id.Value, cancellationToken);
        if (product is null)
        {
            return false;
        }

        product.Name = model.Name;
        product.Category = model.Category;
        product.DefaultPrice = model.DefaultPrice;
        product.IsActive = model.IsActive;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
