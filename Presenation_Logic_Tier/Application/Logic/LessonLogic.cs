using System.Data.SqlTypes;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.LessonDTO;
using Domain.Models;
using Google.Protobuf.Reflection;

namespace Application.Logic;

public class LessonLogic : ILessonLogic
{
    private readonly ILessonClient _lessonClient;

    public LessonLogic(ILessonClient lessonClient)
    {
        _lessonClient = lessonClient;
    }

    public async Task<Lesson> GetByIdAsync(string id)
    {
        return await _lessonClient.GetByIdAsync(id);
    }

    public async Task<int> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto)
    {
        return await _lessonClient.MarkAttendanceAsync(markAttendanceDto);
    }

    public async Task<IEnumerable<User>> GetAttendanceAsync(string id)
    {
        return await _lessonClient.GetAttendanceAsync(id);
    }

    public async Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto)
    {
        var newLesson = new Lesson
        {

            Topic = lessonCreationDto.Topic,
            Date = lessonCreationDto.Date,
            Description = lessonCreationDto.Description
        };

        var createdLesson = await _lessonClient.CreateAsync(lessonCreationDto);

        return createdLesson;
    }

    public async Task<bool> DeleteAsync(string lessonId)
    {
        Lesson? lesson = await _lessonClient.GetByIdAsync(lessonId);
        if (lesson == null)
        {
            throw new Exception($"Lesson with ID {lessonId} was not found!");

        }

        await _lessonClient.DeleteAsync(lessonId);
        return true;
    }


    public async Task UpdateLessonAsync(LessonUpdateDTO lessonUpdateDto)
    {

        var updatedLesson = new Lesson
        {

            Topic = lessonUpdateDto.Topic,
            Date = lessonUpdateDto.Date,
            Description = lessonUpdateDto.Description,
           // Id = "",
           // Homework = null
        };

        await _lessonClient.UpdateLessonAsync(lessonUpdateDto);
    }
}
/* Lesson? lesson = await _lessonClient.GetByIdAsync(lessonUpdateDto.Id);
 
 if (lesson == null)
 {
     throw new Exception($"Can not find Lesson with Id {lessonUpdateDto.Id}");
 }

 // Updating the existing lesson with the values from the update DTO
 var newLesson = new LessonUpdateDTO()
 {
      Id = "",
     Topic = lessonUpdateDto.Topic,
     Date = lessonUpdateDto.Date,
     Description = lessonUpdateDto.Description
     
 };


 await _lessonClient.UpdateLessonAsync(newLesson);*?

 
 
     
 }
 var updatedLesson = new Lesson
 {

     Topic = lessonUpdateDto.Topic,
     Date = lessonUpdateDto.Date,
     Description = lessonUpdateDto.Description,
     Id="",
     Homework = null
 };

 await _lessonClient.UpdateLessonAsync(lessonUpdateDto);


 
 
/* Lesson? lesson = await _lessonClient.GetByIdAsync(lessonUpdateDto.Id);
 
if (lesson == null)
{
    throw new Exception($"Can not find Lesson with Id {lessonUpdateDto.Id}");
}

// Updating the existing lesson with the values from the update DTO
var newLesson = new Lesson
{

    Topic = lessonUpdateDto.Topic,
    Date = lessonUpdateDto.Date,
    Description = lessonUpdateDto.Description
};


var updatedLesson = await _lessonClient.UpdateLessonAsync(lessonUpdateDto);

return updatedLesson;*/
      

    
  
    
