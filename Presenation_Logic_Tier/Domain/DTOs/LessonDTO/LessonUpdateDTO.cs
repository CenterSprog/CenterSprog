using Domain.Models;

namespace Domain.DTOs.LessonDTO;

public class LessonUpdateDTO
{
   public string Id { get; set; }
    
    public long Date{ get; set; }
    //public string ClassId { get; set; }
   
    public string Topic{ get; set; }
    
    public string Description{ get; set; }
    
    
   

    public LessonUpdateDTO( long date, string id,string topic, 
        string description)
    {
       Id = id;
        Date = date;
       //ClassId = classId;
        Topic = topic;
        Description = description;
       
    }
   public LessonUpdateDTO(string id)
    {
       Id = id;
    }

    public LessonUpdateDTO()
    {
    }
}