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
        ValidateFeedbackCreation(addFeedbackDto);
        return await _feedbackClient.AddFeedbackAsync(addFeedbackDto);
    }

    public async Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername)
    {
        return await _feedbackClient.GetFeedbackByHandInIdAndStudentUsernameAsync(handInId, studentUsername);
    }

    private void ValidateFeedbackCreation(AddFeedbackDTO addFeedbackDto)
    {
        int[] allowedGrades = { -3, 0, 2, 4, 7, 10, 12 };

        if (string.IsNullOrEmpty(addFeedbackDto.StudentUsername))
            throw new Exception("Student Username is required");
        if (string.IsNullOrEmpty(addFeedbackDto.HandInId))
            throw new Exception("HandIn Id is required");
        if (!allowedGrades.Contains(addFeedbackDto.Grade))
            throw new Exception("Grade must be one of these numbers: -3, 0, 2, 4, 7, 10, 12");
        if (string.IsNullOrWhiteSpace(addFeedbackDto.Comment) || addFeedbackDto.Comment.Length < 5)
            throw new Exception("Comment must be at least 5 characters long.");
    }
}