using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.infra.Authentication;
using stream_roulette.infra.Persistence.Database;
using stream_roulette.infra.Persistence.Repositories;

namespace stream_roulette.infra;

public static class InfraDI
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDatabase(services, configuration);
        ConfigureAuthentication(services, configuration);
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("DbConnectionString");

        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(dbConnectionString, options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)));

        services.AddScoped<IDonationRepository, DonationRepository>();
    }

    private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.Section).Get<JwtSettings>();
        if (jwtSettings is null)
        {
            throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings are not found.");
        }
        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
    }
}