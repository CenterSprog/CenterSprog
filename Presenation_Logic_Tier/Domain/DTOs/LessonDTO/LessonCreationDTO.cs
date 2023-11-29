namespace Domain.DTOs.LessonDTO;
public class LessonCreationDTO
{
    
    
    public long Date{ get; set; }
    public string classId { get; }
   
    public string Topic{ get; set; }
    
    public string Description{ get; set; }

    public LessonCreationDTO( long date, string topic, 
        string description)
    {
      
        Date = date;
        Topic = topic;
        Description = description;
    }

    public LessonCreationDTO()
    {
    }
}