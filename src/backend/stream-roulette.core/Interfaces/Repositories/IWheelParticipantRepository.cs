using stream_roulette.core.Models.WheelParticipants;

namespace stream_roulette.core.Interfaces.Repositories;

public interface IWheelParticipantRepository
{
    Task AddAsync(WheelParticipant wheelParticipant);

    Task<List<WheelParticipant>> GetAsync();
}