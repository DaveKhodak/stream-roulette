using stream_roulette.api.Requests;
using stream_roulette.core.Models;

namespace stream_roulette.api.Mappers;

internal static class LoginRequestDataMapper
{
    public static LoginRequestData Map(LoginRequest request) => new()
    {
        Username = request.UserName,
        Password = request.Password,
    };
}