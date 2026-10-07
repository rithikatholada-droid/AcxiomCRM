using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models;

public class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Customer name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(100)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Phone is required.")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
    public string Phone { get; set; } = "";

    [StringLength(100)]
    public string? Company { get; set; }

    [StringLength(60)]
    public string? City { get; set; }

    [Required]
    public string Status { get; set; } = "Active";

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
