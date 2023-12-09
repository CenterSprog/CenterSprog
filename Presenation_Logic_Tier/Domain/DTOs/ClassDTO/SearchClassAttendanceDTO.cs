namespace Domain.DTOs.ClassDTO;

public class SearchClassAttendanceDTO
{
    public string Id { get; set; }
    public string Username { get; set; }
    
    public SearchClassAttendanceDTO(string id, string username)
    {
        Id = id;
        Username = username;
    }
}