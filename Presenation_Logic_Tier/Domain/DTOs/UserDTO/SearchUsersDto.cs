namespace Domain.DTOs.UserDTO;

public class SearchUsersDto
{
    public string Role { get; set; }
    public string ClassId { get; set; }

    public SearchUsersDto()
    {
    }

    public SearchUsersDto(string role, string classId)
    {
        Role = role;
        ClassId = classId;
    }
}