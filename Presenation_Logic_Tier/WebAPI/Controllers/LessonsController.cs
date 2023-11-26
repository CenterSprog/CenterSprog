using Application.LogicInterfaces;
using Domain.Models;
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
    
    [HttpGet("{id}", Name = "GetByIdAsync")]
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
}