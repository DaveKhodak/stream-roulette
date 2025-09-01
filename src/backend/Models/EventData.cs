namespace stream_roulette.Models;

public sealed record EventData
{
    public string? TipId { get; init; }
    public required string Username { get; init; }
    public string? ProviderId { get; init; }
    public required string DisplayName { get; init; }
    public required int Amount { get; init; }
    public int Streak { get; init; }
    public object? Tier { get; init; }
    public string? Currency { get; init; }
    public string? Message { get; init; }
    public int? Quantity { get; init; }
    public object[]? Items { get; init; }
    public required string Avatar { get; init; }
}