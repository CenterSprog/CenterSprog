using Domain.Models;

namespace Domain.DTOs.HomeworkDTO;

public class HomeworkHandInDTO
{
    public string HomeworkId { get; set; }
    public string StudentUsername { get; set; }
    public string Answer { get; set; }

    public HomeworkHandInDTO(string homeworkId, string studentUsername, string answer)
    {
        HomeworkId = homeworkId;
        StudentUsername = studentUsername;
        Answer = answer;
    }
}