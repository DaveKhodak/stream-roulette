using Microsoft.AspNetCore.Mvc;
using stream_roulette.api.Mappers;
using stream_roulette.core.Interfaces.Repositories;

namespace stream_roulette.api.Controllers;

[ApiController]
[Route("api/donations")]
public sealed class DonationsController(IDonationRepository donationsRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await donationsRepository.GetAsync();
        return Ok(result.Select(GetDonationsResponseMapper.Map));
    }
}