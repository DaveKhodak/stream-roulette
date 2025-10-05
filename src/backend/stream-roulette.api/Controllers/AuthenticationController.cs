using Microsoft.AspNetCore.Mvc;
using stream_roulette.api.Mappers;
using stream_roulette.api.Requests;
using stream_roulette.infra.Authentication;

namespace stream_roulette.api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController(
    IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var tokenResponse = await authenticationService.LoginAsync(LoginRequestDataMapper.Map(request));
        if (tokenResponse == null)
        {
            return this.BadRequest();
        }

        return this.Ok(TokenResponseDtoMapper.Map(tokenResponse));
    }

    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokenResponse = await authenticationService.RefreshTokenAsync(RefreshTokenRequestDataMapper.Map(request));
        if (tokenResponse == null)
        {
            return this.BadRequest();
        }

        return this.Ok(TokenResponseDtoMapper.Map(tokenResponse));
    }
}