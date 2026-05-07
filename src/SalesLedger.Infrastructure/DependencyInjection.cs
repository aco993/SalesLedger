using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesLedger.Application.Interfaces;
using SalesLedger.Infrastructure.Data;
using SalesLedger.Infrastructure.Services;

namespace SalesLedger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ISellerService, SellerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IImportService, ImportService>();
        services.AddScoped<IReportService, ReportService>();

        return services;
    }
}
