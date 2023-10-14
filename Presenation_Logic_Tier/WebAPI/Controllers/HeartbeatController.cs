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
    public async Task<ActionResult<String>> CreateAsync(PulseCreationDTO pulseCreation)
    {
        try
        {
            String pulse = await heartbeatLogic.CreateAsync(pulseCreation);
            return Ok(pulse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<int>> GetAsync()
    {
        try
        {
            int heartbeats = await heartbeatLogic.GetAsync();
            return Ok(heartbeats.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}