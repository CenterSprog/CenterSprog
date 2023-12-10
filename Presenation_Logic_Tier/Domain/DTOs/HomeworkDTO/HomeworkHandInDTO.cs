using Domain.Models;

namespace Domain.DTOs.HomeworkDTO;

public class HomeworkHandInDTO
{
    public string HomeworkId { get; set; }
    public string StudentUsername { get; set; }
    public HandInHomework HandInHomework { get; set; }

    public HomeworkHandInDTO(string homeworkId, string studentUsername, HandInHomework handInHomework)
    {
        HomeworkId = homeworkId;
        StudentUsername = studentUsername;
        HandInHomework = handInHomework;
    }
}