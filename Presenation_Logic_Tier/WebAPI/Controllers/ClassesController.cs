﻿using Application.LogicInterfaces;
using Domain.DTOs.ClassDTO;
using Domain.DTOs.LessonDTO;
using Domain.DTOs.UserDTO;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassLogic _classLogic;

    public ClassesController(IClassLogic classLogic)
    {
        _classLogic = classLogic;
    }

    [HttpGet("{id}", Name = "GetClassByIdAsync")]
    public async Task<ActionResult<ClassEntity>> GetByIdAsync([FromRoute] string id)
    {
        try
        {
            ClassEntity classEntity = await _classLogic.GetByIdAsync(id);
            return Ok(classEntity);
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassEntity>>> GetAllAsync([FromQuery] string? username)
    {
        try
        {   
            SearchClassDTO dto = new SearchClassDTO(username);
            IEnumerable<ClassEntity> classes = await _classLogic.GetAllAsync(dto);

            return Ok(classes);
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

    [HttpGet("{id}/participants", Name = "GetAllParticipantsAsync")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllParticipantsAsync([FromRoute] string id, [FromQuery] string? role)
    {
        try
        {
            SearchClassParticipantsDTO dto = new SearchClassParticipantsDTO(id, role);
            IEnumerable<User> participants = await _classLogic.GetAllParticipantsAsync(dto);

            return Ok(participants);
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

    [HttpPost]
    public async Task<ActionResult<ClassEntity>> CreateAsync([FromBody] ClassCreationDTO dto)
    {
        try
        {
            ClassEntity? createdClass = await _classLogic.CreateAsync(dto);
            return Created($"classes/{createdClass.Id}", createdClass);
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

    [HttpPatch("{id}", Name = "UpdateAsync")]
    [Authorize("MustBeAdmin")]
    public async Task<ActionResult<Boolean>> UpdateAsync([FromBody] ClassUpdateDTO dto)
    {
        try
        {
            Boolean result = await _classLogic.UpdateAsync(dto);
            if (result == false)
                throw new Exception("Failed to update from webapi");
            return Ok(result);

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
    [HttpGet("{id}/attendances", Name = "GetClassAttendanceAsync")]
    public async Task<ActionResult<IEnumerable<UserAttendanceDTO>>> GetClassAttendanceAsync([FromRoute] string id, [FromQuery] string? username)
    {
        try
        {
            if (!string.IsNullOrEmpty(username))
            {
                var dto = new SearchClassAttendanceDTO(id, username);
                var lessons = await _classLogic.GetClassAttendanceByUsernameAsync(dto);

                if (lessons == null)
                    return NotFound();

                return Ok(lessons);
            }
            var attendees = await _classLogic.GetClassAttendanceAsync(id);

            return Ok(attendees);
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