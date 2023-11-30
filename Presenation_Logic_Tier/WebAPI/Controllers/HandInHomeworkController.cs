using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class HandInHomeworkController : ControllerBase
{
    private readonly IHandInHomeworkLogic _handInHomeworkLogic;

    public HandInHomeworkController(IHandInHomeworkLogic handInHomeworkLogic)
    {
        _handInHomeworkLogic = handInHomeworkLogic;
    }

    [HttpPost]
    public async Task<ActionResult<HandInHomework>> HandInHomework(HomeworkHandInDTO dto)
    {
        try
        {
            HandInHomework handedInHomework = await _handInHomeworkLogic.HandInHomework(dto);
            return Created($"/handInHomework/{handedInHomework.Id}, {handedInHomework.Answer}", handedInHomework);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }
}