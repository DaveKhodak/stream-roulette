namespace stream_roulette.api.Responses;

internal sealed record GetDonationsResponse
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }

    public required string Message { get; init; }

    public required int Amount { get; init; }

    public required DateTime CreatedAt { get; init; }
}