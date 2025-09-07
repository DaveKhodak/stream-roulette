namespace stream_roulette.core.Models.WheelParticipants;

public sealed record WheelParticipant
{
    public required string Id { get; init; }
    public required string Username { get; init; }
    public required string DisplayName { get; init; }
    public required string Message { get; init; }
    public required int Amount { get; init; }
    public string? Currency { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

}