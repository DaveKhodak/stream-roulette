using stream_roulette.Models.Donations;

namespace stream_roulette.Persistence.Repositories;

internal sealed class DonationRepository(DatabaseContext dbContext) : IDonationsRepository
{
    public async Task AddAsync(Donation donation)
    {
        await dbContext.AddAsync(donation);
        await dbContext.SaveChangesAsync();
    }
}