namespace stream_roulette.Configuration;

public class StreamElementsOptions
{
    public const string Section = "StreamElements";

    public required string Jwt { get; init; }
}