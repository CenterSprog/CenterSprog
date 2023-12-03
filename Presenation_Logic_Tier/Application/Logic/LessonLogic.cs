using System.Data.SqlTypes;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.LessonDTO;
using Domain.Models;

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
     
   

/*
    public async Task UpdateAsync(LessonUpdateDTO updateDto)
    {
        Lesson? existingLesson = await _lessonClient.GetByIdAsync(updateDto.Id);

        if (existingLesson != null)
        {
            existingLesson.Id = updateDto.Id;
        }
        throw new Exception("Cannot update Lesson");
    }*/
    
}