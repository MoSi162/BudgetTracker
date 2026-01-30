using BudgetTracker.Api.Domain;

namespace BudgetTracker.Api.Persistence;

/// <summary>
/// Simple repository contract for budget entries.
/// </summary>
public interface IBudgetEntryRepository
{
    Task<IEnumerable<BudgetEntry>> GetAllAsync();
    Task<BudgetEntry?> GetByIdAsync(Guid id);
    Task AddAsync(BudgetEntry entry);
    Task UpdateAsync(BudgetEntry entry);
    Task DeleteAsync(Guid id);
}
