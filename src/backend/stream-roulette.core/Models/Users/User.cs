namespace stream_roulette.core.Models.Users;

public sealed record User
{
    public required string Id { get; init; }
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
}