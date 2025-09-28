using Microsoft.EntityFrameworkCore;
using stream_roulette.core.Interfaces.Repositories;
using stream_roulette.core.Models.Donations;
using stream_roulette.infra.Persistence.Database;

namespace stream_roulette.infra.Persistence.Repositories;

internal sealed class DonationRepository(DatabaseContext dbContext) : IDonationRepository
{
    public async Task AddAsync(Donation donation)
    {
        await dbContext.AddAsync(donation);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Donation>> GetAsync()
    {
        return await dbContext.Donations.ToListAsync();
    }
}