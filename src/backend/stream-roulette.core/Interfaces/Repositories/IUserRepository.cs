using stream_roulette.core.Models.Users;

namespace stream_roulette.core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);

    Task<User?> GetByUsernameAsync(string username);

    Task UpdateAsync(User user);
}