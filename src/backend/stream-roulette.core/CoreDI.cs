using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stream_roulette.core.Configuration;

namespace stream_roulette.core;

public static class CoreDI
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        services.Configure<StreamElementsOptions>(configuration.GetSection(StreamElementsOptions.Section));
    }
}