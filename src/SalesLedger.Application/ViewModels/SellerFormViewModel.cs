using System.ComponentModel.DataAnnotations;

namespace SalesLedger.Application.ViewModels;

public class SellerFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [StringLength(160)]
    [Display(Name = "Company / Store")]
    public string? CompanyName { get; set; }

    [StringLength(160)]
    [Display(Name = "Contact Data")]
    public string? ContactData { get; set; }

    [StringLength(600)]
    public string? Notes { get; set; }
}
