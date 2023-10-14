using Domain.DTOs.HeartbeatDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IHeartbeatLogic
{
    Task<Heartbeat> CreateAsync(PulseCreationDTO pulseToCreate);
    Task<IEnumerable<Heartbeat>> GetAsync();

}