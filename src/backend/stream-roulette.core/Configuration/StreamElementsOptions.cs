namespace stream_roulette.core.Configuration;

public class StreamElementsOptions
{
    public const string Section = "StreamElements";

    public required string Jwt { get; init; }
}