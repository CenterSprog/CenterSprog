namespace Domain.Models;

public class ClassEntity
{
    public String Id { get; set; }
    public String Title { get; set; }
    public String Room { get; set; }

    public List<User> Participants { get; set; }

    public List<Lesson> lessons { get; set; } = new List<Lesson>();
    

    public ClassEntity(string id, string title, string room)
    {
        Participants = new List<User>();
        Id = id;
        Title = title;
        Room = room;
    }
    

    public ClassEntity()
    {
    }
    
    public void CreateLesson(Lesson lesson)
    {
        lessons.Add(lesson);
    }
    
    
    public void EditLesson(string Id, Lesson updatedLesson)
    {
       
        Lesson existingLesson = lessons.FirstOrDefault(l => l.Id == Id);

       
        if (existingLesson != null)
        {
            existingLesson.Topic = updatedLesson.Topic;
            existingLesson.Date = updatedLesson.Date;
            existingLesson.Description = updatedLesson.Description;
        }
        throw new ArgumentException($"Lesson with ID {Id} not found and can not be edited.");
    }
    
    
    
    public void RemoveLesson(string Id)
    {
        
        var lessonToRemove = lessons.FirstOrDefault(l => l.Id == Id);

        
        if (lessonToRemove != null)
        {
            lessons.Remove(lessonToRemove);
        }
        else
        {
            throw new ArgumentException($"Lesson with ID {Id} not found.");
            
        }
    }
}