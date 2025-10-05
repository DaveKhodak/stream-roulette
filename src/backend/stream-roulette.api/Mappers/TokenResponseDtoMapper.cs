using stream_roulette.api.Requests;
using stream_roulette.api.Responses;
using stream_roulette.core.Models;
using stream_roulette.infra.Authentication;

namespace stream_roulette.api.Mappers;

internal static class TokenResponseDtoMapper
{
    public static TokenResponseDto Map(TokenResponse response) => new()
    {
        AccessToken = response.AccessToken,
        RefreshToken = response.RefreshToken,
    };
}