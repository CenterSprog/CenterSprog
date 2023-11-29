namespace Domain.DTOs.UserDTO;

public class UserCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public string Role { get; set; }

    public UserCreationDto()
    {
    }

    
}