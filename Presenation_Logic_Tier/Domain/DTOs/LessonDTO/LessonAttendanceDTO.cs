namespace Domain.DTOs.LessonDTO;

public class LessonAttendanceDTO
{
    public long Date { get; set; }
    public string Topic { get; set; }
    public bool HasAttended { get; set; }

    public LessonAttendanceDTO()
    {
    }
}