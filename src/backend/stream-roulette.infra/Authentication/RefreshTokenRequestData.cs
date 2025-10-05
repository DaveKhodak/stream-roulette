namespace stream_roulette.infra.Authentication;

public class RefreshTokenRequestData
{
    public required string UserId { get; init; }

    public required string RefreshToken { get; init; }
}