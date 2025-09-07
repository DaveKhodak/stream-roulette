using stream_roulette.api.Responses;
using stream_roulette.core.Models.WheelParticipants;

namespace stream_roulette.api.Mappers;

internal static class WheelParticipantResponseMapper
{
    public static WheelParticipantResponse Map(WheelParticipant participant) => new()
    {
        Name = participant.Message,
        Value = participant.Amount
    };
}