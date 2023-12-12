using IHandInHomeworkLogic = Application.LogicInterfaces.IHandInHomeworkLogic;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using Grpc.Core;
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
    public async Task<ActionResult<HandInHomework>> HandInHomework([FromBody] HomeworkHandInDTO dto)
    {
        try
        {
            HandInHomework handedInHomework = await _handInHomeworkLogic.HandInHomework(dto);
            return Created($"/handIns/{handedInHomework.Id}, {handedInHomework.Answer}", handedInHomework);
        }
        catch (RpcException e)
        {
            return NotFound(e.Status.Detail);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{homeworkId}", Name = "GetHandInsByHomeworkIdAsync")]
    public async Task<ActionResult<IEnumerable<HandInHomework>>> GetHandInsByHomeworkIdAsync([FromRoute] string homeworkId)
    {
        try
        {
            IEnumerable<HandInHomework> handIns = await _handInHomeworkLogic.GetHandInsByHomeworkIdAsync(homeworkId);
            return new OkObjectResult(handIns);
        }
        catch (RpcException e)
        {
            return NotFound(e.Status.Detail);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{homeworkId}/student/{username}", Name = "GetHandInByHomeworkIdAndStudentUsernameAsync")]
    public async Task<ActionResult<HandInHomework>> GetHandInByHomeworkIdAndStudentUsernameAsync([FromRoute] string homeworkId,
        [FromRoute] string username)
    {
        try
        {
            HandInHomework handIn = await _handInHomeworkLogic.GetHandInByHomeworkIdAndStudentUsernameAsync(homeworkId, username);

            return Ok(handIn);
        }
        catch (RpcException e)
        {
            return NotFound(e.Status.Detail);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}