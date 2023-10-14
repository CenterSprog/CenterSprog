using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.HeartbeatDTO;
using Domain.Models;

namespace Application.Logic;

public class HeartbeatLogic:IHeartbeatLogic
{
    private readonly IHeartbeatDAO heartbeatDao;

    public HeartbeatLogic(IHeartbeatDAO heartbeatDao)
    {
        this.heartbeatDao = heartbeatDao;
    }
    
    public async Task<Heartbeat> CreateAsync(PulseCreationDTO pulseToCreate)
    {
        Heartbeat toCreate = new Heartbeat
        {
            Pulse = pulseToCreate.Pulse
        };
        Heartbeat created = await heartbeatDao.CreateAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<Heartbeat>> GetAsync()
    {
        return heartbeatDao.GetAsync();
    }
}