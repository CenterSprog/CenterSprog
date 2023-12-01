namespace Domain.DTOs.ClassDTO;

public class SearchClassDTO
{
    public string? Username { get; set; }
    
    public SearchClassDTO(string? username)
    {
        Username = username;
    }
}