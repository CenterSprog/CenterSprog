using Domain.DTOs.FeedbackDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IFeedbackService
{
    Task<Feedback> AddFeedbackAsync(AddFeedbackDTO addFeedbackDto, string token);
    Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername);
}