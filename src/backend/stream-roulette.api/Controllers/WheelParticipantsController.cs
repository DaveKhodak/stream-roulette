using Microsoft.AspNetCore.Mvc;
using stream_roulette.api.Mappers;
using stream_roulette.core.Interfaces.Repositories;

namespace stream_roulette.api.Controllers;

[ApiController]
[Route("api/wheel-participants")]
public sealed class WheelParticipantsController(IWheelParticipantRepository donationsRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await donationsRepository.GetAsync();
        return Ok(result.Select(WheelParticipantResponseMapper.Map));
    }
}