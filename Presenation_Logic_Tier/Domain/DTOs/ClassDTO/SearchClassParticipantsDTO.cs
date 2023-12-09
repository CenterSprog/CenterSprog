namespace Domain.DTOs.ClassDTO;

public class SearchClassParticipantsDTO
{
    public string Id { get; set; }
    public string? Role { get; set; }
    
    public SearchClassParticipantsDTO(string id, string? role)
    {
        Id = id;
        Role = role;
    }
}