using System.ComponentModel.DataAnnotations;

namespace SalesLedger.Application.ViewModels;

public class ProductFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(160)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Category { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    [Display(Name = "Default Price")]
    public decimal? DefaultPrice { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
