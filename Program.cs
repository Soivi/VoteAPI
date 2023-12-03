using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CandidateDb>(opt => opt.UseInMemoryDatabase("CandidateList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/candidates", async (CandidateDb db) =>
    await db.Candidates.ToListAsync());

app.MapGet("/candidates/{id}", async (int id, CandidateDb db) =>
    await db.Candidates.FindAsync(id)
        is Candidate candidate
            ? Results.Ok(candidate)
            : Results.NotFound());

app.MapPost("/candidates", async (Candidate candidate, CandidateDb db) =>
{
    db.Candidates.Add(candidate);
    await db.SaveChangesAsync();

    return Results.Created($"/candidate/{candidate.Id}", candidate);
});


app.MapGet("/", () => "Hello World!");

app.Run();