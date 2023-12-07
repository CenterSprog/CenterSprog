using Domain.DTOs.FeedbackDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IFeedbackService
{
    Task<Feedback> AddFeedback(AddFeedbackDTO addFeedbackDto);
    Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername);
}