namespace stream_roulette.api.Requests;

public sealed record RefreshTokenRequest
{
    public required string UserId { get; init; }

    public required string RefreshToken { get; init; }
}