using System.ComponentModel.DataAnnotations;
using BudgetTracker.Api.Domain;

namespace BudgetTracker.Api.DTOs;

/// <summary>Request DTO for updating a budget entry.</summary>
public class UpdateBudgetEntryDto
{
    [Required]
    public BudgetCategory? Category { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
    public decimal? Amount { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    public string? Notes { get; set; }

    public void ApplyToDomain(Domain.BudgetEntry entry)
    {
        entry.Category = Category!.Value;
        entry.Name = Name!;
        entry.Amount = Amount!.Value;
        entry.Date = Date!.Value;
        entry.Notes = Notes;
    }
}
