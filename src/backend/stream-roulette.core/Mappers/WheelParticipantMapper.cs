using stream_roulette.core.Models;
using stream_roulette.core.Models.WheelParticipants;

namespace stream_roulette.core.Mappers;

public static class WheelParticipantMapper
{
    public static WheelParticipant Map(StreamEvent mappedEvent) => new()
    {
        Id = mappedEvent._id,
        Username = mappedEvent.Data.Username,
        DisplayName = mappedEvent.Data.DisplayName,
        Message = mappedEvent.Data.Message ?? string.Empty,
        Amount = mappedEvent.Data.Amount,
        Currency = mappedEvent.Data.Currency,
        CreatedAt = mappedEvent.CreatedAt,
        UpdatedAt = mappedEvent.UpdatedAt
    };
}