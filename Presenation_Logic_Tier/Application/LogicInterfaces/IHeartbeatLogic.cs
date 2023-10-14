using Domain.DTOs.HeartbeatDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IHeartbeatLogic
{
    Task<String> CreateAsync(PulseCreationDTO pulseToCreate);
    Task<int> GetAsync();

}