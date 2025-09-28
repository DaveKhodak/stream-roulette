using stream_roulette.core.Models.Donations;

namespace stream_roulette.core.Models.WheelParticipants;

public sealed record WheelParticipant
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }
    public required int Amount { get; init; }
    public required ICollection<Donation> Donations { get; init; }
}