using stream_roulette.core.Models.Users;

namespace stream_roulette.infra.Authentication;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}