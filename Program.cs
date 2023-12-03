using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CandidateDb>(opt => opt.UseInMemoryDatabase("CandidateList"));
builder.Services.AddDbContext<VoteDb>(opt => opt.UseInMemoryDatabase("VoteList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Candidates
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

// Votes
app.MapGet("/votes", async (VoteDb db) =>
    await db.Votes.ToListAsync());

app.MapGet("/votes/{id}", async (int id, VoteDb db) =>
    await db.Votes.FindAsync(id)
        is Vote vote
            ? Results.Ok(vote)
            : Results.NotFound());

app.MapPost("/votes", async (Vote vote, VoteDb db) =>
{
    db.Votes.Add(vote);
    await db.SaveChangesAsync();

    return Results.Created($"/vote/{vote.Id}", vote);
});

app.MapGet("/votecount", async (VoteDb db) =>
    await db.Votes.CountAsync());

app.MapGet("/votecount/{candidateId}", async (int candidateId, VoteDb db) =>
    await db.Votes.CountAsync(Vote => Vote.CandidateId == candidateId));

app.MapGet("/", () => "Hello World!");
app.Run();