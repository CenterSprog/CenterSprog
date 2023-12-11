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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{id}/attendance", Name = "MarkAttendanceAsync")]
    public async Task<ActionResult<int>> MarkAttendanceAsync([FromRoute] string id, List<String> studentUsernames)
    {
        try
        {
            MarkAttendanceDTO markAttendanceDto = new(id, studentUsernames);
            int amountOfParticipants = await _lessonLogic.MarkAttendanceAsync(markAttendanceDto);
            return Ok(amountOfParticipants.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id}/attendance", Name = "GetAttendanceAsync")]
    public async Task<ActionResult<IEnumerable<User>>> GetAttendanceAsync([FromRoute] string id)
    {
        try
        {
            IEnumerable<User> attendees = await _lessonLogic.GetAttendanceAsync(id);

            if (attendees == null)
                return NotFound();

            return Ok(attendees);
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
            Lesson? createdLesson = await _lessonLogic.CreateAsync(lessonCreationDto);
            if (createdLesson == null)
                throw new Exception("Failed to create new Lesson in lesson controller");
            return Created($"lessons/{createdLesson.Id}", createdLesson);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed create lesson controller : {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{lessonId}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string lessonId)
    {
        try
        {
            bool deleted = await _lessonLogic.DeleteAsync(lessonId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
        catch (Exception ex)
        {

            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
    [HttpPatch]
    public async Task<ActionResult> UpdateLessonAsync(LessonUpdateDTO lessonUpdateDto)
    {
        try
        {
            await _lessonLogic.UpdateLessonAsync(lessonUpdateDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}