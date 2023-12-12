using Application.LogicInterfaces;
using Domain.DTOs.FeedbackDTO;
using Domain.Models;
using Grpc.Core;
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
    public async Task<ActionResult<Feedback>> AddFeedback([FromBody] AddFeedbackDTO addFeedbackDto)
    {
        try
        {
            Feedback createdFeedback = await _feedbackLogic.AddFeedbackAsync(addFeedbackDto);

            return Created($"/feedbacks/{createdFeedback.Id}", createdFeedback);
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

    [HttpGet ("{handInId}/student/{username}", Name = "GetFeedbackByHomeworkIdAndStudentUsernameAsync")]
    public async Task<ActionResult<Feedback>> GetFeedbackByHandInIdAndStudentUsernameAsync([FromRoute] string handInId,
        [FromRoute] string username)
    {
        try
        {
            Feedback feedback = await _feedbackLogic.GetFeedbackByHandInIdAndStudentUsernameAsync(handInId, username);

            return Ok(feedback);
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