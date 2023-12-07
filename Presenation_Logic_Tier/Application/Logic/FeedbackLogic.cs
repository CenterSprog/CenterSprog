using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.FeedbackDTO;
using Domain.Models;

namespace Application.Logic;

public class FeedbackLogic : IFeedbackLogic
{
    private readonly IFeedbackClient _feedbackClient;

    public FeedbackLogic(IFeedbackClient feedbackClient)
    {
        _feedbackClient = feedbackClient;
    }

    public async Task<Feedback> AddFeedbackAsync(AddFeedbackDTO addFeedbackDto)
    {
        return await _feedbackClient.AddFeedbackAsync(addFeedbackDto);
    }

    public async Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername)
    {
        return await _feedbackClient.GetFeedbackByHandInIdAndStudentUsernameAsync(handInId, studentUsername);
    }
}