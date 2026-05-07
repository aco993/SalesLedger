using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Domain.Entities;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class SellerService(AppDbContext context) : ISellerService
{
    public Task<SellerFormViewModel> GetCreateModelAsync() => Task.FromResult(new SellerFormViewModel());

    public async Task<IReadOnlyList<SellerListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sellers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SellerListItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CompanyName = x.CompanyName,
                ContactData = x.ContactData,
                TransactionsCount = x.Transactions.Count,
                TotalSalesAmount = x.Transactions.Sum(t => (decimal?)t.TotalAmount) ?? 0m
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<SellerFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Sellers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new SellerFormViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CompanyName = x.CompanyName,
                ContactData = x.ContactData,
                Notes = x.Notes
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CreateAsync(SellerFormViewModel model, CancellationToken cancellationToken = default)
    {
        var seller = new Seller
        {
            Name = model.Name,
            CompanyName = model.CompanyName,
            ContactData = model.ContactData,
            Notes = model.Notes
        };

        await context.Sellers.AddAsync(seller, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(SellerFormViewModel model, CancellationToken cancellationToken = default)
    {
        if (!model.Id.HasValue)
        {
            return false;
        }

        var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Id == model.Id.Value, cancellationToken);
        if (seller is null)
        {
            return false;
        }

        seller.Name = model.Name;
        seller.CompanyName = model.CompanyName;
        seller.ContactData = model.ContactData;
        seller.Notes = model.Notes;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
