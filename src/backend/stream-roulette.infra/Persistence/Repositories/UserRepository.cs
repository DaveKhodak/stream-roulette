using Microsoft.EntityFrameworkCore;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.core.Models.Users;
using stream_roulette.infra.Persistence.Database;

namespace stream_roulette.infra.Persistence.Repositories;

internal sealed record UserRepository(DatabaseContext dbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}