namespace Domain.DTOs.LessonDTO;

public class LessonUpdateDTO
{
    public string LessonId { get;  }
    
    public long Date{ get; }
   
    public string Topic{ get;  }
    
    public string Description{ get; }
   

    public LessonUpdateDTO(string lessonId, long date, string topic, string description)
    {
        LessonId = lessonId;
        Date = date;
        Topic = topic;
        Description = description;
    }

}