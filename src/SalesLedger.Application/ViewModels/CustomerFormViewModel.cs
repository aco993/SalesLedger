using System.ComponentModel.DataAnnotations;

namespace SalesLedger.Application.ViewModels;

public class CustomerFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [StringLength(160)]
    [Display(Name = "Contact Data")]
    public string? ContactData { get; set; }

    [StringLength(120)]
    public string? City { get; set; }

    [StringLength(400)]
    public string? Notes { get; set; }
}
