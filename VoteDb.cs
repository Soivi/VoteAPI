using Microsoft.EntityFrameworkCore;

class VoteDb : DbContext
{
    public VoteDb(DbContextOptions<VoteDb> options)
        : base(options) { }

    public DbSet<Vote> Votes => Set<Vote>();
}