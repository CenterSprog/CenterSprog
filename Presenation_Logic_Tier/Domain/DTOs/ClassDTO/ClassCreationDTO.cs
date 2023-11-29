namespace Domain.DTOs.ClassDTO;

public class ClassCreationDTO
{
    public string Title { get; set; }
    public string Room { get; set; }

    public ClassCreationDTO()
    {
    }

    public ClassCreationDTO(string title, string room)
    {
        Title = title;
        Room = room;
    }
}