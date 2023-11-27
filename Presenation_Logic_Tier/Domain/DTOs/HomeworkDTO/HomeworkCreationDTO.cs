namespace Domain.DTOs.HomeworkDTO;

public class HomeworkCreationDTO
{
  
    public long Deadline { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LessonId { get; set; }

    public HomeworkCreationDTO()
    {
   
    }

    public HomeworkCreationDTO(string lessonId)
    {
        LessonId = lessonId;
    }

    public HomeworkCreationDTO(string lessonId, long deadline, string title, string description)
    {
        LessonId = lessonId;
        Deadline = deadline;
        Title = title;
        Description = description;
    }
}