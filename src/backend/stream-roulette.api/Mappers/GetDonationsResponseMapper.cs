using stream_roulette.api.Responses;
using stream_roulette.core.Models.Donations;

namespace stream_roulette.api.Mappers;

internal static class GetDonationsResponseMapper
{
    public static GetDonationsResponse Map(Donation participant) => new()
    {
        Id = participant.Id,
        DisplayName = participant.DisplayName,
        Message = participant.Message,
        Amount = participant.Amount,
        CreatedAt = participant.CreatedAt,
    };
}