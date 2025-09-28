using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using stream_roulette.core.Interfaces.Repositories;

namespace stream_roulette.api.Controllers;

[ApiController]
[Route("api/authentication")]
public sealed class AuthenticationController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return Ok();
    }
}