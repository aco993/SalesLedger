using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Domain.Entities;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class CustomerService(AppDbContext context) : ICustomerService
{
    public Task<CustomerFormViewModel> GetCreateModelAsync() => Task.FromResult(new CustomerFormViewModel());

    public async Task<IReadOnlyList<CustomerListItemViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Customers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CustomerListItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ContactData = x.ContactData,
                City = x.City,
                TransactionsCount = x.Transactions.Count
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Customers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CustomerFormViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ContactData = x.ContactData,
                City = x.City,
                Notes = x.Notes
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CreateAsync(CustomerFormViewModel model, CancellationToken cancellationToken = default)
    {
        await context.Customers.AddAsync(new Customer
        {
            Name = model.Name,
            ContactData = model.ContactData,
            City = model.City,
            Notes = model.Notes
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(CustomerFormViewModel model, CancellationToken cancellationToken = default)
    {
        if (!model.Id.HasValue)
        {
            return false;
        }

        var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == model.Id.Value, cancellationToken);
        if (customer is null)
        {
            return false;
        }

        customer.Name = model.Name;
        customer.ContactData = model.ContactData;
        customer.City = model.City;
        customer.Notes = model.Notes;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
