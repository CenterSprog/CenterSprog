using Domain.Models;

namespace Application.DAOInterfaces;

public interface IHeartbeatDAO
{
    Task<String> CreateAsync(Heartbeat heartbeat);
    Task<int> GetAsync();
}