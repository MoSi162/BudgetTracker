using System.ComponentModel.DataAnnotations;
using BudgetTracker.Api.Domain;

namespace BudgetTracker.Api.DTOs;

/// <summary>Request DTO for creating a budget entry.</summary>
public class CreateBudgetEntryDto
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

    public Domain.BudgetEntry ToDomain() => new()
    {
        Id = Guid.NewGuid(),
        Category = Category!.Value,
        Name = Name!,
        Amount = Amount!.Value,
        Date = Date!.Value,
        Notes = Notes
    };
}
