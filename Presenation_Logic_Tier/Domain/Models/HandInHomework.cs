namespace Domain.Models;

public class HandInHomework
{
    public string Id { get; set; }
    public string Answer { get; set; }

    public HandInHomework(string id, string answer)
    {
        Id = id;
        Answer = answer;
    }

    public HandInHomework()
    {
    }

}