namespace stream_roulette.api.Requests;

public sealed record LoginRequest
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}