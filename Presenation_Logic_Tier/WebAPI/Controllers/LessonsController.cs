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

    [HttpPost("{id}/Attendance", Name = "AddAttendanceAsync")]
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonsByClassIdAsync([FromRoute] string id, List<String> studentUsernames)
    {
        try
        {
            AddAttendanceDTO addAttendanceDto = new(id, studentUsernames);
            int amountOfParticipants = await _lessonLogic.AddAttendance(addAttendanceDto);
            return Ok(amountOfParticipants.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
  /*  
    
    [HttpGet("lessons", Name = "GetAsync")]
    public async Task<IActionResult> GetAsync([FromQuery] SearchLessonParametersDTO searchParameters)
    {
        try
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            IEnumerable<Lesson> lessons = await _lessonLogic.GetAsync(searchParameters);

           
            if (lessons == null || !lessons.Any())
            {
                return NotFound();
            }

            
            return Ok(lessons);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

   */ 
    
/*
    [HttpPost("lessons", Name = "CreateAsync")]
    public async Task<IActionResult> CreateAsync([FromBody] LessonCreationDTO lessonCreationDto)
    {
        try
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            Lesson createdLesson = await _lessonLogic.CreateAsync(lessonCreationDto);

            
            return CreatedAtRoute("GetAsync", new { id = createdLesson.Id }, createdLesson);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }*/

/*
   
    [HttpPut("lessons", Name = "UpdateAsync")]//{lessonId}
    public async Task<ActionResult> UpdateAsync( [FromBody] LessonUpdateDTO lessonUpdateDto)
    {
        try
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            await _lessonLogic.UpdateAsync(lessonUpdateDto);

           
            return NoContent();
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
*/
   
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




    


}