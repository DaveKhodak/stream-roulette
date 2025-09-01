
using System.Text.Json;
using H.Socket.IO;
using Microsoft.Extensions.Options;
using stream_roulette.Configuration;
using stream_roulette.Mappers;
using stream_roulette.Models;
using stream_roulette.Persistence.Repositories;

namespace stream_roulette.Services.Donations;

public class DonationsBackgroundService(
    IDonationsRepository donationsRepository,
    IOptions<StreamElementsOptions> streamElementsOptions,
    ILogger<DonationsBackgroundService> logger) : IHostedService, IDisposable
{
    private Timer? timer = null;
    private SocketIoClient? client;

    private JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DonationsBackgroundService)} has started");

        client = new SocketIoClient();
        client.Connected += (sender, args) => Console.WriteLine($"Connected: {args.Namespace}");
        client.Disconnected += (sender, args) => Console.WriteLine($"Disconnected. Reason: {args.Reason}, Status: {args.Status:G}");
        client.EventReceived += (sender, args) => Console.WriteLine($"EventReceived: Namespace: {args.Namespace}, Value: {args.Value}, IsHandled: {args.IsHandled}");
        client.HandledEventReceived += (sender, args) => Console.WriteLine($"HandledEventReceived: Namespace: {args.Namespace}, Value: {args.Value}");
        client.UnhandledEventReceived += (sender, args) => Console.WriteLine($"UnhandledEventReceived: Namespace: {args.Namespace}, Value: {args.Value}");
        client.ErrorReceived += (sender, args) => Console.WriteLine($"ErrorReceived: Namespace: {args.Namespace}, Value: {args.Value}");
        client.ExceptionOccurred += (sender, args) => Console.WriteLine($"ExceptionOccurred: {args.Exception}");

        client.On("connect", () =>
        {
            Console.WriteLine("You are logged in.");
        });

        client.On("event", async (data) =>
        {
            var responseEvent = JsonSerializer.Deserialize<StreamEvent>(data, options);
            if (responseEvent is not null && responseEvent.Type == "tip")
            {
                await donationsRepository.AddAsync(DonationMapper.Map(responseEvent));
            }
        });

        await client.ConnectAsync(new Uri("https://realtime.streamelements.com"));
        await client.Emit("authenticate", new { method = "jwt", token = streamElementsOptions.Value.Jwt });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DonationsBackgroundService)} has stopped");

        if (client is not null)
        {
            await client.DisposeAsync();
        }
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}