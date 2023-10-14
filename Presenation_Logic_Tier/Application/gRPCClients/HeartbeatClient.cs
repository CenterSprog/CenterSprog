using Application.DAOInterfaces;
using Domain.Models;

namespace Application.gRPCClients;

public class HeartbeatClient:IHeartbeatDAO
{
    public Task<Heartbeat> CreateAsync(Heartbeat heartbeat)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Heartbeat>> GetAsync()
    {
        throw new NotImplementedException();
    }
}