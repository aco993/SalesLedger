namespace SalesLedger.Application.DTOs;

public class ProductSalesDto
{
    public string ProductName { get; set; } = string.Empty;

    public int QuantitySold { get; set; }

    public decimal TotalAmount { get; set; }
}
