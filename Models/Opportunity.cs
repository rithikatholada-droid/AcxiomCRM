using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models;

public class Opportunity : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Opportunity name is required.")]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please select a customer.")]
    [Display(Name = "Customer")]
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [Range(0.01, 1000000000, ErrorMessage = "Opportunity Amount must be greater than 0.")]
    public double Amount { get; set; }

    [Required]
    public string Stage { get; set; } = "Qualification";

    [Range(0, 100, ErrorMessage = "Probability must be between 0 and 100.")]
    [Display(Name = "Probability (%)")]
    public int Probability { get; set; }

    [Required(ErrorMessage = "Expected close date is required.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Expected Close Date")]
    public DateTime ExpectedCloseDate { get; set; } = DateTime.Today.AddDays(30);

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Business rule: weighted value = Amount x Probability / 100
    public double WeightedValue => Amount * Probability / 100.0;

    // Business rule (server side): active opportunities cannot have a past close date
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        bool active = Stage != "Won" && Stage != "Lost";
        if (active && ExpectedCloseDate.Date < DateTime.Today)
            yield return new ValidationResult("Expected Close Date cannot be in the past.",
                new[] { nameof(ExpectedCloseDate) });
    }
}
