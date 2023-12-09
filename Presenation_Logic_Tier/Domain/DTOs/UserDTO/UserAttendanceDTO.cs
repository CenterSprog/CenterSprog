namespace Domain.DTOs.UserDTO;

public class UserAttendanceDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public double TotalAbsence { get; set; }

    public UserAttendanceDTO()
    {
    }
}