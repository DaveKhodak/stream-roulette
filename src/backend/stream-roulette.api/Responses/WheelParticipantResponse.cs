namespace stream_roulette.api.Responses;

internal sealed record WheelParticipantResponse
{
    public required string Name { get; init; }

    public required int Value { get; init; }
}