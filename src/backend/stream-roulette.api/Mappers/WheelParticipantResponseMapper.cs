using stream_roulette.api.Responses;
using stream_roulette.core.Models.Donations;

namespace stream_roulette.api.Mappers;

internal static class WheelParticipantResponseMapper
{
    public static WheelParticipantResponse Map(Donation participant) => new()
    {
        Name = participant.Message,
        Value = participant.Amount
    };
}