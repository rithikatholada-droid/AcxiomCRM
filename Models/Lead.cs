using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models;

public class Lead
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lead name is required.")]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Phone is required.")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
    public string Phone { get; set; } = "";

    [StringLength(100)]
    public string? Company { get; set; }

    [Required]
    public string Source { get; set; } = "Website";

    [Required(ErrorMessage = "Lead status is required.")]
    public string Status { get; set; } = "New";

    [Range(0, 10000000, ErrorMessage = "Expected value must be between 0 and 1,00,00,000.")]
    [Display(Name = "Expected Value")]
    public double ExpectedValue { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
