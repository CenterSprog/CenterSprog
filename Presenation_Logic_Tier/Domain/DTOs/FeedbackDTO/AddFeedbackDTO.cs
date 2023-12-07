using Domain.Models;

namespace Domain.DTOs.FeedbackDTO;

public class AddFeedbackDTO
{
    public string HandInId { get; set; }
    public string StudentUsername { get; set; }
    public Feedback Feedback { get; set; }

    public AddFeedbackDTO(string handInId, string studentUsername, Feedback feedback)
    {
        HandInId = handInId;
        StudentUsername = studentUsername;
        Feedback = feedback;
    }
}