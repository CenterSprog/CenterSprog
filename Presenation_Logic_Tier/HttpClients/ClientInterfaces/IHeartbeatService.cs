using Domain.DTOs.HeartbeatDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHeartbeatService
{
    Task<String> CreateAsync(PulseCreationDTO pulseCreation);
    Task<int> GetAsync();
}