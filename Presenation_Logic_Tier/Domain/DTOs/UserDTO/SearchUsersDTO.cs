namespace Domain.DTOs.UserDTO;

public class SearchUsersDTO
{
    public string Role { get; set; }
    public string ClassId { get; set; }

    public SearchUsersDTO()
    {
    }

    public SearchUsersDTO(string role, string classId)
    {
        Role = role;
        ClassId = classId;
    }
}