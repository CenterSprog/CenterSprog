namespace Domain.DTOs.LessonDTO;

public class AddAttendanceDTO
{
    public string LessonId { get; set; }
    public List<String> StudentUsernames { get; set; }

    public AddAttendanceDTO(string lessonId, List<String> studentUsernames)
    {
        LessonId = lessonId;
        StudentUsernames = studentUsernames;
    }
}