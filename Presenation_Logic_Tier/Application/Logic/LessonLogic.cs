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
        ValidateLessonCreationAndUpdate(lessonCreationDto.Topic, lessonCreationDto.Description, lessonCreationDto.Date,
            lessonCreationDto.ClassId);
        var createdLesson = await _lessonClient.CreateAsync(lessonCreationDto);

        return createdLesson;
    }

    public async Task<Boolean> DeleteAsync(string lessonId)
    {
        return await _lessonClient.DeleteAsync(lessonId);
    }

    public async Task<Boolean> UpdateLessonAsync(LessonUpdateDTO lessonUpdateDto)
    {
        ValidateLessonCreationAndUpdate(lessonUpdateDto.Topic, lessonUpdateDto.Description, lessonUpdateDto.Date,
            lessonUpdateDto.Id);
        return await _lessonClient.UpdateLessonAsync(lessonUpdateDto);
    }

    public void ValidateLessonCreationAndUpdate(string topic, string description, long date, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new Exception("Id is required.");
        if (string.IsNullOrWhiteSpace(topic) || topic.Length < 3)
            throw new Exception("Topic must be at least 3 characters long.");
        if (string.IsNullOrWhiteSpace(description) || description.Length < 10)
            throw new Exception("Description must be at least 10 characters long.");
        if (date == 0)
            throw new Exception("Date is required.");
    }
}