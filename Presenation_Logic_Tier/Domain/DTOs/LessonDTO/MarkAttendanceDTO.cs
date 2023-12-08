namespace Domain.DTOs.LessonDTO;

public class MarkAttendanceDTO
{
    public string LessonId { get; set; }
    public List<String> StudentUsernames { get; set; }

    public MarkAttendanceDTO(string lessonId, List<String> studentUsernames)
    {
        LessonId = lessonId;
        StudentUsernames = studentUsernames;
    }
}