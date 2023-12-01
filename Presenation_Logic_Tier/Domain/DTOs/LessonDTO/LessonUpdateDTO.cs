namespace Domain.DTOs.LessonDTO;

public class LessonUpdateDTO
{
    public string Id { get;  }
    
    public long Date{ get; }
   
    public string Topic{ get;  }
    
    public string Description{ get; }
   

    public LessonUpdateDTO(string id, long date, string topic, string description)
    {
       Id = id;
        Date = date;
        Topic = topic;
        Description = description;
    }
    public LessonUpdateDTO(){}

}