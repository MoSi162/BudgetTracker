using BudgetTracker.Api.Domain;

namespace BudgetTracker.Api.Persistence;

/// <summary>
/// In-memory repository. Not thread-safe for heavy concurrent use but fine for local dev / learning.
/// </summary>
public class InMemoryBudgetEntryRepository : IBudgetEntryRepository
{
    private readonly List<BudgetEntry> _entries = new();
    private readonly object _lock = new();

    public InMemoryBudgetEntryRepository()
    {
        // Seed a couple of entries for convenience
        _entries.Add(new BudgetEntry
        {
            Id = Guid.NewGuid(),
            Category = BudgetCategory.Income,
            Name = "Salary",
            Amount = 3000m,
            Date = DateTime.UtcNow.AddDays(-5),
            Notes = "Monthly salary"
        });

        _entries.Add(new BudgetEntry
        {
            Id = Guid.NewGuid(),
            Category = BudgetCategory.Subscription,
            Name = "Streaming Service",
            Amount = 12.99m,
            Date = DateTime.UtcNow.AddDays(-10),
            Notes = "Monthly"
        });
    }

    public Task<IEnumerable<BudgetEntry>> GetAllAsync()
    {
        lock (_lock)
        {
            // Return a copy to avoid external mutation
            return Task.FromResult(_entries.Select(e => e).AsEnumerable());
        }
    }

    public Task<BudgetEntry?> GetByIdAsync(Guid id)
    {
        lock (_lock)
        {
            return Task.FromResult(_entries.FirstOrDefault(e => e.Id == id));
        }
    }

    public Task AddAsync(BudgetEntry entry)
    {
        lock (_lock)
        {
            _entries.Add(entry);
        }

        return Task.CompletedTask;
    }

    public Task UpdateAsync(BudgetEntry entry)
    {
        lock (_lock)
        {
            var idx = _entries.FindIndex(e => e.Id == entry.Id);
            if (idx >= 0)
            {
                _entries[idx] = entry;
            }
            else
            {
                throw new KeyNotFoundException($"Entry with id {entry.Id} not found.");
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        lock (_lock)
        {
            var removed = _entries.RemoveAll(e => e.Id == id);
            if (removed == 0)
            {
                throw new KeyNotFoundException($"Entry with id {id} not found.");
            }
        }

        return Task.CompletedTask;
    }
}
