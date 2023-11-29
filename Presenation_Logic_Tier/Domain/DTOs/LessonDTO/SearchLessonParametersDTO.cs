namespace Domain.DTOs.LessonDTO;

public class SearchLessonParametersDTO
{

   
    public string? LessonId { get;  }
    
    public long Date{ get; }
   
    public string? Topic{ get;  }
    
    public string? Description{ get; }
   

    public SearchLessonParametersDTO(string? lessonId, long date, string? topic, string? description)
    {
        LessonId = lessonId;
        Date = date;
        Topic = topic;
        Description = description;
    }
    
   

}
