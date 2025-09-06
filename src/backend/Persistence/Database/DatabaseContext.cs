using Microsoft.EntityFrameworkCore;
using stream_roulette.Models.Donations;

public class DatabaseContext : DbContext
{
    public DbSet<Donation> Donations { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
    {
        
    }
}