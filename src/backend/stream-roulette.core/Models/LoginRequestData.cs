namespace stream_roulette.core.Models;

public sealed record LoginRequestData
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}