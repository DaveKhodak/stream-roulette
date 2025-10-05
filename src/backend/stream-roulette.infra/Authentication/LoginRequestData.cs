namespace stream_roulette.infra.Authentication;

public sealed record LoginRequestData
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}