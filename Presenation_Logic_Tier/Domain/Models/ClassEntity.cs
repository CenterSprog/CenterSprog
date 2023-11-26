namespace Domain.Models;

public class ClassEntity
{
    public String Id { get; set; }
    public String Title { get; set; }
    public String Room { get; set; }

    public ClassEntity(string id, string title, string room)
    {
        Id = id;
        Title = title;
        Room = room;
    }
}