using Microsoft.EntityFrameworkCore;

class CandidateDb : DbContext
{
    public CandidateDb(DbContextOptions<CandidateDb> options)
        : base(options) { }

    public DbSet<Candidate> Candidates => Set<Candidate>();
}