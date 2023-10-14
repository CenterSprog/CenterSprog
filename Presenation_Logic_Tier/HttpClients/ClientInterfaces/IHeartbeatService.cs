using Domain.DTOs.HeartbeatDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHeartbeatService
{
    Task<Heartbeat> Create(PulseCreationDTO pulseCreation);
    Task<IEnumerable<Heartbeat>> Get();
}