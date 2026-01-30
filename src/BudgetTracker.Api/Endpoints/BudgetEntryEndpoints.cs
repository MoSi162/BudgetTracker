using BudgetTracker.Api.DTOs;
using BudgetTracker.Api.Domain;
using BudgetTracker.Api.Persistence;

namespace BudgetTracker.Api.Endpoints;

/// <summary>
/// Minimal API endpoints for budget entries grouped under /api/budget-entries
/// 
/// This keeps controllers out of the way while remaining simple for a junior dev.
/// </summary>
public static class BudgetEntryEndpoints
{
    public static void MapBudgetEntryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/budget-entries").WithTags("BudgetEntries");

        group.MapGet("/", async (IBudgetEntryRepository repo) =>
        {
            var items = await repo.GetAllAsync();
            return Results.Ok(items.Select(DTOs.BudgetEntryDto.FromDomain));
        });

        group.MapGet("/{id:guid}", async (Guid id, IBudgetEntryRepository repo) =>
        {
            var item = await repo.GetByIdAsync(id);
            return item is null ? Results.NotFound() : Results.Ok(BudgetEntryDto.FromDomain(item));
        });

        group.MapPost("/", async (CreateBudgetEntryDto dto, IBudgetEntryRepository repo) =>
        {
            var entry = dto.ToDomain();
            await repo.AddAsync(entry);
            return Results.Created($"/api/budget-entries/{entry.Id}", BudgetEntryDto.FromDomain(entry));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateBudgetEntryDto dto, IBudgetEntryRepository repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            dto.ApplyToDomain(existing);
            await repo.UpdateAsync(existing);
            return Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IBudgetEntryRepository repo) =>
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return Results.NotFound();
            await repo.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
