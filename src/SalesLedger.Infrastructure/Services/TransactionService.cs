using Microsoft.EntityFrameworkCore;
using SalesLedger.Application.Interfaces;
using SalesLedger.Application.ViewModels;
using SalesLedger.Domain.Entities;
using SalesLedger.Infrastructure.Data;

namespace SalesLedger.Infrastructure.Services;

public class TransactionService(AppDbContext context) : ITransactionService
{
    public async Task<TransactionIndexViewModel> GetIndexViewModelAsync(TransactionFilterViewModel filter, CancellationToken cancellationToken = default)
    {
        var query = context.Transactions
            .Include(x => x.Seller)
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .AsNoTracking()
            .AsQueryable();

        if (filter.FromDate.HasValue)
        {
            query = query.Where(x => x.TransactionDate >= filter.FromDate.Value);
        }

        if (filter.ToDate.HasValue)
        {
            var toDate = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(x => x.TransactionDate <= toDate);
        }

        if (filter.SellerId.HasValue)
        {
            query = query.Where(x => x.SellerId == filter.SellerId.Value);
        }

        if (filter.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == filter.ProductId.Value);
        }

        if (filter.SourceType.HasValue)
        {
            query = query.Where(x => x.SourceType == filter.SourceType.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var term = filter.SearchTerm.Trim().ToLower();
            query = query.Where(x =>
                x.Product.Name.ToLower().Contains(term) ||
                x.Seller.Name.ToLower().Contains(term) ||
                (x.Customer != null && x.Customer.Name.ToLower().Contains(term)) ||
                (x.BuyerName != null && x.BuyerName.ToLower().Contains(term)));
        }

        var transactions = await query
            .OrderByDescending(x => x.TransactionDate)
            .Select(x => new TransactionListItemViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                SellerName = x.Seller.Name,
                ProductName = x.Product.Name,
                CustomerDisplayName = x.Customer != null ? x.Customer.Name : (x.BuyerName ?? "Not provided"),
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalAmount = x.TotalAmount,
                PaymentType = x.PaymentType,
                SourceType = x.SourceType
            })
            .ToListAsync(cancellationToken);

        return new TransactionIndexViewModel
        {
            Filter = filter,
            Transactions = transactions,
            SellerOptions = await GetSellerOptionsAsync(cancellationToken),
            ProductOptions = await GetProductOptionsAsync(cancellationToken)
        };
    }

    public async Task<TransactionFormViewModel> GetCreateModelAsync(CancellationToken cancellationToken = default)
    {
        return new TransactionFormViewModel
        {
            SellerOptions = await GetSellerOptionsAsync(cancellationToken),
            ProductOptions = await GetProductOptionsAsync(cancellationToken),
            CustomerOptions = await GetCustomerOptionsAsync(cancellationToken)
        };
    }

    public async Task<TransactionFormViewModel?> GetEditModelAsync(int id, CancellationToken cancellationToken = default)
    {
        var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (transaction is null)
        {
            return null;
        }

        return new TransactionFormViewModel
        {
            Id = transaction.Id,
            TransactionDate = transaction.TransactionDate,
            SellerId = transaction.SellerId,
            CustomerId = transaction.CustomerId,
            BuyerName = transaction.BuyerName,
            ProductId = transaction.ProductId,
            Quantity = transaction.Quantity,
            UnitPrice = transaction.UnitPrice,
            TotalAmount = transaction.TotalAmount,
            PaymentType = transaction.PaymentType,
            SourceType = transaction.SourceType,
            Notes = transaction.Notes,
            SellerOptions = await GetSellerOptionsAsync(cancellationToken),
            ProductOptions = await GetProductOptionsAsync(cancellationToken),
            CustomerOptions = await GetCustomerOptionsAsync(cancellationToken)
        };
    }

    public async Task<TransactionListItemViewModel?> GetDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Transactions
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new TransactionListItemViewModel
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                SellerName = x.Seller.Name,
                ProductName = x.Product.Name,
                CustomerDisplayName = x.Customer != null ? x.Customer.Name : (x.BuyerName ?? "Not provided"),
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalAmount = x.TotalAmount,
                PaymentType = x.PaymentType,
                SourceType = x.SourceType
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> CreateAsync(TransactionFormViewModel model, CancellationToken cancellationToken = default)
    {
        var entity = new Transaction
        {
            TransactionDate = model.TransactionDate,
            SellerId = model.SellerId,
            CustomerId = model.CustomerId,
            BuyerName = model.BuyerName,
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            UnitPrice = model.UnitPrice,
            TotalAmount = model.TotalAmount,
            PaymentType = model.PaymentType,
            SourceType = model.SourceType,
            Notes = model.Notes
        };

        await context.Transactions.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(TransactionFormViewModel model, CancellationToken cancellationToken = default)
    {
        if (!model.Id.HasValue)
        {
            return false;
        }

        var entity = await context.Transactions.FirstOrDefaultAsync(x => x.Id == model.Id.Value, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.TransactionDate = model.TransactionDate;
        entity.SellerId = model.SellerId;
        entity.CustomerId = model.CustomerId;
        entity.BuyerName = model.BuyerName;
        entity.ProductId = model.ProductId;
        entity.Quantity = model.Quantity;
        entity.UnitPrice = model.UnitPrice;
        entity.TotalAmount = model.TotalAmount;
        entity.PaymentType = model.PaymentType;
        entity.SourceType = model.SourceType;
        entity.Notes = model.Notes;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Transactions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Transactions.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<IReadOnlyList<LookupItemViewModel>> GetSellerOptionsAsync(CancellationToken cancellationToken)
        => await context.Sellers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemViewModel
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync(cancellationToken);

    private async Task<IReadOnlyList<LookupItemViewModel>> GetProductOptionsAsync(CancellationToken cancellationToken)
        => await context.Products
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemViewModel
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync(cancellationToken);

    private async Task<IReadOnlyList<LookupItemViewModel>> GetCustomerOptionsAsync(CancellationToken cancellationToken)
        => await context.Customers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupItemViewModel
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync(cancellationToken);
}
