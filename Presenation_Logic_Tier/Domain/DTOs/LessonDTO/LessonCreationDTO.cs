namespace Domain.DTOs.LessonDTO;
public class LessonCreationDTO
{
    
    
    public long Date{ get; set; }
    public string ClassId { get; set; }
   
    public string Topic{ get; set; }
    
    public string Description{ get; set; }
    

   

    public LessonCreationDTO( long date, string topic, 
        string description, string classId)
    {
      
        Date = date;
        ClassId = classId;
        Topic = topic;
        Description = description;
    }
    public LessonCreationDTO(string classId)
         {
             ClassId = classId;
         }

    public LessonCreationDTO()
    {
    }
}