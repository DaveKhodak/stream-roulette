namespace stream_roulette.infra.Authentication;

public class TokenResponse
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }
}