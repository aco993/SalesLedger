namespace SalesLedger.Application.DTOs;

public class MonthlySalesDto
{
    public string Label { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }
}
