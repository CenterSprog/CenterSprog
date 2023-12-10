using Application.LogicInterfaces;
using Domain.DTOs.FeedbackDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbacksController : ControllerBase
{
    private readonly IFeedbackLogic _feedbackLogic;

    public FeedbacksController(IFeedbackLogic feedbackLogic)
    {
        _feedbackLogic = feedbackLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Feedback>> AddFeedback(AddFeedbackDTO addFeedbackDto)
    {
        try
        {
            Feedback? createdFeedback = await _feedbackLogic.AddFeedbackAsync(addFeedbackDto);
            if (createdFeedback == null)
                throw new Exception("Failed to create a new feedback in feedback controller");
            return Created($"/feedbacks/{createdFeedback.Id}", createdFeedback);
        }
        
        catch (Exception e)
        {
            Console.WriteLine($"Failed to add feedback controller : {e.Message}");
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("{handInId}/{studentUsername}", Name = "GetFeedbackByHomeworkIdAndStudentUsernameAsync")]
    public async Task<ActionResult<Feedback>> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId,
        string studentUsername)
    {
        try
        {
            Feedback? feedback = await _feedbackLogic.GetFeedbackByHandInIdAndStudentUsernameAsync(handInId, studentUsername);

            if (feedback != null)
            {
                return Ok(feedback);
            }
            else
            {
                return NotFound($"No feedback found for HandInId: {handInId} and StudentUsername: {studentUsername}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get feedback from controller: {e.Message}");
            return StatusCode(500, e.Message);
        }
    }
}