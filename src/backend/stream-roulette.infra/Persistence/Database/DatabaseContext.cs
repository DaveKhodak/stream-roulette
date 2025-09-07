using Microsoft.EntityFrameworkCore;
using stream_roulette.core.Models.WheelParticipants;

namespace stream_roulette.infra.Persistence.Database;

public class DatabaseContext : DbContext
{
    public DbSet<WheelParticipant> WheelParticipants { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
    {

    }
}