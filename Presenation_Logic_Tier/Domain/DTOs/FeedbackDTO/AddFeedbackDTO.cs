namespace Domain.DTOs.FeedbackDTO;

public class AddFeedbackDTO
{
    public string HandInId { get; set; }
    public string StudentUsername { get; set; }
    public int Grade { get; set; }
    public string Comment { get; set; }

    public AddFeedbackDTO(string handInId, string studentUsername, int grade, string comment)
    {
        HandInId = handInId;
        StudentUsername = studentUsername;
        Grade = grade;
        Comment = comment;
    }
}