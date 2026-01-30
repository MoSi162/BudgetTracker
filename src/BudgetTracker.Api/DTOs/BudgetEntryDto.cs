using BudgetTracker.Api.Domain;

namespace BudgetTracker.Api.DTOs;

/// <summary>Response DTO for budget entries.</summary>
public class BudgetEntryDto
{
    public Guid Id { get; set; }
    public BudgetCategory Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }

    public static BudgetEntryDto FromDomain(Domain.BudgetEntry entry) => new()
    {
        Id = entry.Id,
        Category = entry.Category,
        Name = entry.Name,
        Amount = entry.Amount,
        Date = entry.Date,
        Notes = entry.Notes
    };
}
