using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class HandInsController : ControllerBase
{
    private readonly IHandInHomeworkLogic _handInHomeworkLogic;

    public HandInsController(IHandInHomeworkLogic handInHomeworkLogic)
    {
        _handInHomeworkLogic = handInHomeworkLogic;
    }

    [HttpPost]
    public async Task<ActionResult<HandInHomework>> HandInHomework(HomeworkHandInDTO dto)
    {
        try
        {
            HandInHomework handedInHomework = await _handInHomeworkLogic.HandInHomework(dto);
            return Created($"/handIns/{handedInHomework.Id}, {handedInHomework.Answer}", handedInHomework);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }

    [HttpGet("{homeworkId}", Name = "GetHandInsByHomeworkIdAsync")]
    public async Task<ActionResult<IEnumerable<HandInHomework>>> GetHandInsByHomeworkIdAsync(string homeworkId)
    {
        try
        {
            IEnumerable<HandInHomework> handIns = await _handInHomeworkLogic.GetHandInsByHomeworkIdAsync(homeworkId);
            return new OkObjectResult(handIns);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{homeworkId}/{studentUsername}", Name = "GetHandInByHomeworkIdAndStudentUsernameAsync")]
    public async Task<ActionResult<HandInHomework>> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId,
        string studentUsername)
    {
        try
        {
            HandInHomework handIn = await _handInHomeworkLogic.GetHandInByHomeworkIdAndStudentUsernameAsync(homeworkId, studentUsername);

            if (handIn == null)
            {
                return NotFound();
            }

            return Ok(handIn);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}