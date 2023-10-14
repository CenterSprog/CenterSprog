using Domain.Models;

namespace Application.DAOInterfaces;

public interface IHeartbeatDAO
{
    Task<Heartbeat> CreateAsync(Heartbeat heartbeat);
    Task<IEnumerable<Heartbeat>> GetAsync();
}