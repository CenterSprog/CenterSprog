using Application.LogicInterfaces;
using Domain.DTOs.ClassDTO;
using Domain.Models;
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassEntity>>> GetAllAsync([FromQuery] string? username)
    {
        try
        {   
            Console.WriteLine($"Classes for : {username}");
            SearchClassDTO dto = new SearchClassDTO(username);
            IEnumerable<ClassEntity> classes = await _classLogic.GetAllAsync(dto);

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

    [HttpPost]
    public async Task<ActionResult<ClassEntity>> CreateAsync(ClassCreationDTO dto)
    {
        try
        {
            ClassEntity? createdClass = await _classLogic.CreateAsync(dto);
            if (createdClass == null)
                throw new Exception("Failed to create new class in class controller");
            return Created($"classes/{createdClass.Id}", createdClass);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed create class controller : {e.Message}");
            return StatusCode(500,e.Message);
        }
    }
}