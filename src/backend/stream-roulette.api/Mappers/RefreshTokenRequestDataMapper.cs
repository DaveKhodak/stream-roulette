using stream_roulette.api.Requests;
using stream_roulette.core.Models;
using stream_roulette.infra.Authentication;

namespace stream_roulette.api.Mappers;

internal static class RefreshTokenRequestDataMapper
{
    public static RefreshTokenRequestData Map(RefreshTokenRequest request) => new()
    {
        UserId = request.UserId,
        RefreshToken = request.RefreshToken,
    };
}