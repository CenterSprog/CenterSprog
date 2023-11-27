using Application.LogicInterfaces;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly ILessonLogic _lessonLogic;

    public LessonController(ILessonLogic lessonLogic)
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

    [HttpGet("Class/{classId}", Name = "GetLessonsByClassIdAsync")]
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonsByClassIdAsync([FromRoute] string classId)
    {
        try
        {
            IEnumerable<Lesson> lessons = await _lessonLogic.GetLessonsByClassIdAsync(classId);
            return new OkObjectResult(lessons);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


}