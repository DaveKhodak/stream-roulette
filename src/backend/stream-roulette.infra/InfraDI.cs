using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.infra.Persistence.Database;
using stream_roulette.infra.Persistence.Repositories;

namespace stream_roulette.infra;

public static class InfraDI
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped<IWheelParticipantRepository, WheelParticipantRepository>();
    }
}