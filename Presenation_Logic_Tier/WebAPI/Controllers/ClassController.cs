using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ClassController : ControllerBase
{
    private readonly IClassLogic _classLogic;

    public ClassController(IClassLogic classLogic)
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("byUsername/{username}", Name = "GetByUsernameAsync")]
    public async Task<ActionResult<IEnumerable<ClassEntity>>> GetByUsernameAsync([FromRoute] string username)
    {
        try
        {
            IEnumerable<ClassEntity> classes = await _classLogic.GetByUsernameAsync(username);

            if (classes == null || !classes.Any())
            {
                return NotFound();
            }

            return Ok(classes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}