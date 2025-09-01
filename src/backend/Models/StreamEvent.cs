namespace stream_roulette.Models;

public sealed record StreamEvent
{
    public required string _id { get; init; }
    public required string Channel { get; init; }
    public required string Type { get; init; }
    public required string Provider { get; init; }
    public string? Flagged { get; init; }
    public required EventData Data { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

}