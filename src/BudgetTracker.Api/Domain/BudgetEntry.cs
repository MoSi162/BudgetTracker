namespace BudgetTracker.Api.Domain;

/// <summary>
/// Represents a single budget entry (income, subscription, necessary cost, or savings).
/// </summary>
public class BudgetEntry
{
    /// <summary>Unique identifier</summary>
    public Guid Id { get; set; }

    /// <summary>Category (Income, Subscription, etc.)</summary>
    public BudgetCategory Category { get; set; }

    /// <summary>Short name or description</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Amount of money (positive for income, positive for costs)</summary>
    public decimal Amount { get; set; }

    /// <summary>Date of the entry (e.g., booking date)</summary>
    public DateTime Date { get; set; }

    /// <summary>Optional notes</summary>
    public string? Notes { get; set; }
}
