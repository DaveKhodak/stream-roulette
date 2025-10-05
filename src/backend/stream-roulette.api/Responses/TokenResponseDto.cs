namespace stream_roulette.api.Responses;

public class TokenResponseDto
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }
}