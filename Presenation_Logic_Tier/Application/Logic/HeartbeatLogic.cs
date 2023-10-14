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
    
    public async Task<String> CreateAsync(PulseCreationDTO pulseToCreate)
    {
        Heartbeat toCreate = new Heartbeat
        {
            Pulse = pulseToCreate.Pulse
        };
        String created = await heartbeatDao.CreateAsync(toCreate);
        return created;
    }

    public Task<int> GetAsync()
    {
        return heartbeatDao.GetAsync();
    }
}