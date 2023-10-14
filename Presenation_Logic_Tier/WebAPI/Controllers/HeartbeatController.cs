using Application.LogicInterfaces;
using Domain.DTOs.HeartbeatDTO;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class HeartbeatController:ControllerBase
{
    private readonly IHeartbeatLogic heartbeatLogic;

    public HeartbeatController(IHeartbeatLogic heartbeatLogic)
    {
        this.heartbeatLogic = heartbeatLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Heartbeat>> CreateAsync(PulseCreationDTO pulseCreation)
    {
        try
        {
            Heartbeat heartbeat = await heartbeatLogic.CreateAsync(pulseCreation);
            return Ok(heartbeat);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Heartbeat>> GetAsync()
    {
        try
        {
            IEnumerable<Heartbeat> heartbeats = await heartbeatLogic.GetAsync();
            return Ok(heartbeats);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}