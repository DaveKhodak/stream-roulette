using stream_roulette.Models.Donations;

namespace stream_roulette.Persistence.Repositories;

public interface IDonationsRepository
{
    Task AddAsync(Donation donation);
}