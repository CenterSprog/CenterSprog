using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeworksController : ControllerBase
{
    private readonly IHomeworkLogic _homeworkLogic;

    public HomeworksController(IHomeworkLogic homeworkLogic)
    {
        _homeworkLogic = homeworkLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Homework>> CreateHomework([FromBody] HomeworkCreationDTO dto)
    {
        try
        {
            Homework createdHomework = await _homeworkLogic.CreateAsync(dto);
            return Created($"/homeworks/{createdHomework.Id}", createdHomework);
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