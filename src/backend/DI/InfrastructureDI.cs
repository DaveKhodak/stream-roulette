using Microsoft.EntityFrameworkCore;
using stream_roulette.Persistence.Repositories;

namespace stream_roulette.DI;

public static class InfrastructureDI
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped<IDonationsRepository, DonationRepository>();
    }
}