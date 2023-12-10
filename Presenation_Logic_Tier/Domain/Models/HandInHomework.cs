namespace Domain.Models;

public class HandInHomework
{
    public string Id { get; set; }
    public string Answer { get; set; }
    public string StudentUsername { get; set; }


    public HandInHomework(string id, string answer)
    {
        Id = id;
        Answer = answer;
    }

    public HandInHomework()
    {
    }

    public HandInHomework(string id, string answer, string studentUsername)
    {
        Id = id;
        Answer = answer;
        StudentUsername = studentUsername;
    }
}