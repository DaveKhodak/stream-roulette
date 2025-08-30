
using H.Socket.IO;
using Microsoft.Extensions.Options;
using stream_roulette.Configuration;

namespace stream_roulette.Services.Donations;

public class DonationsBackgroundService(
    IOptions<StreamElementsOptions> streamElementsOptions,
    ILogger<DonationsBackgroundService> logger) : IHostedService, IDisposable
{
    private Timer? timer = null;
    private SocketIoClient? client;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
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
        await client.ConnectAsync(new Uri("https://realtime.streamelements.com"));

        logger.LogInformation($"{nameof(DonationsBackgroundService)} has started");

        logger.LogInformation($"{streamElementsOptions.Value.Jwt}");

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