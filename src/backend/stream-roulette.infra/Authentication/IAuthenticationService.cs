using stream_roulette.core.Models;

namespace stream_roulette.infra.Authentication;

public interface IAuthenticationService
{
    Task<TokenResponse?> LoginAsync(LoginRequestData loginData);
}