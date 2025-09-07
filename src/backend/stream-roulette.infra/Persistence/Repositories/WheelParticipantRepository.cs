using Microsoft.EntityFrameworkCore;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.core.Models.WheelParticipants;
using stream_roulette.infra.Persistence.Database;

namespace stream_roulette.infra.Persistence.Repositories;

internal sealed class WheelParticipantRepository(DatabaseContext dbContext) : IWheelParticipantRepository
{
    public async Task AddAsync(WheelParticipant wheelParticipant)
    {
        await dbContext.AddAsync(wheelParticipant);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<WheelParticipant>> GetAsync()
    {
        return await dbContext.WheelParticipants.ToListAsync();
    }
}