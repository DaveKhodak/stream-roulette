using stream_roulette.core.Models.Donations;

namespace stream_roulette.core.Interfaces.Repositories;

public interface IDonationRepository
{
    Task AddAsync(Donation wheelParticipant);

    Task<List<Donation>> GetAsync();
}