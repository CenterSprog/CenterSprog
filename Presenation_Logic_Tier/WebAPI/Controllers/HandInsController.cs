﻿using IHandInHomeworkLogic = Application.LogicInterfaces.IHandInHomeworkLogic;
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
    public async Task<ActionResult<HandInHomework>> HandInHomework(HomeworkHandInDTO dto)
    {
        try
        {
            HandInHomework handedInHomework = await _handInHomeworkLogic.HandInHomework(dto);
            return Created($"/handIns/{handedInHomework.Id}, {handedInHomework.Answer}", handedInHomework);
        }
        catch (RpcException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
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
        catch (RpcException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
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
        catch (RpcException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

}