using stream_roulette.Models.Donations;

namespace stream_roulette.Persistence.Repositories;

internal sealed class DonationRepository : IDonationsRepository
{
    private List<Donation> dbContext = [];
    public async Task AddAsync(Donation donation)
    {
        await Task.CompletedTask;
        dbContext.Add(donation);
    }
}