using BudgetTracker.Api.Endpoints;
using BudgetTracker.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register the in-memory repository (singleton for simplicity)
builder.Services.AddSingleton<IBudgetEntryRepository, InMemoryBudgetEntryRepository>();

var app = builder.Build();

// Enable Swagger in development
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

// Map all budget entry endpoints
app.MapBudgetEntryEndpoints();

app.Run();
