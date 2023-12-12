using Application.LogicInterfaces;
using Domain.DTOs.LessonDTO;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonLogic _lessonLogic;

    public LessonsController(ILessonLogic lessonLogic)
    {
        _lessonLogic = lessonLogic;
    }

    [HttpGet("{id}", Name = "GetLessonByIdAsync")]
    public async Task<ActionResult<Lesson>> GetByIdAsync([FromRoute] string id)
    {
        try
        {
            Lesson lesson = await _lessonLogic.GetByIdAsync(id);
            return Ok(lesson);
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

    [HttpPost("{id}/attendance", Name = "MarkAttendanceAsync")]
    public async Task<ActionResult<int>> MarkAttendanceAsync([FromRoute] string id, [FromBody] List<String> studentUsernames)
    {
        try
        {
            MarkAttendanceDTO markAttendanceDto = new(id, studentUsernames);
            int amountOfParticipants = await _lessonLogic.MarkAttendanceAsync(markAttendanceDto);
            return Ok(amountOfParticipants.ToString());
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
    [HttpGet("{id}/attendance", Name = "GetAttendanceAsync")]
    public async Task<ActionResult<IEnumerable<User>>> GetAttendanceAsync([FromRoute] string id)
    {
        try
        {
            IEnumerable<User> attendees = await _lessonLogic.GetAttendanceAsync(id);

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

    [HttpPost]
    public async Task<ActionResult<Lesson>> CreateAsync([FromBody] LessonCreationDTO lessonCreationDto)
    {
        try
        {
            Lesson createdLesson = await _lessonLogic.CreateAsync(lessonCreationDto);

            return Created($"lessons/{createdLesson.Id}", createdLesson);
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

    [HttpDelete("{lessonId}")]
    public async Task<ActionResult<Boolean>> DeleteAsync([FromRoute] string lessonId)
    {
        try
        { 
          var deleted = await _lessonLogic.DeleteAsync(lessonId);
           
          return Ok(deleted);
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
    [HttpPut]
    public async Task<ActionResult> UpdateLessonAsync([FromBody] LessonUpdateDTO lessonUpdateDto)
    {
        try
        {
            await _lessonLogic.UpdateLessonAsync(lessonUpdateDto);
            return Ok();
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