using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models;

public class FollowUp : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    [StringLength(150)]
    public string Subject { get; set; } = "";

    [Required(ErrorMessage = "Follow-up date is required.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Follow-Up Date")]
    public DateTime FollowUpDate { get; set; } = DateTime.Today.AddDays(1);

    [Required]
    public string Type { get; set; } = "Call";

    [Required]
    public string Status { get; set; } = "Planned";

    [StringLength(500)]
    public string? Remarks { get; set; }

    [Display(Name = "Customer")]
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [Display(Name = "Lead")]
    public int? LeadId { get; set; }
    public Lead? Lead { get; set; }

    // Business rule: a planned follow-up cannot be dated before today
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (Status == "Planned" && FollowUpDate.Date < DateTime.Today)
            yield return new ValidationResult("Follow-up date cannot be earlier than today.",
                new[] { nameof(FollowUpDate) });
    }
}
