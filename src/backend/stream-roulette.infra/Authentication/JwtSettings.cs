namespace stream_roulette.infra.Authentication;

internal sealed record JwtSettings
{
    public const string Section = "JwtSettings"; 
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
}