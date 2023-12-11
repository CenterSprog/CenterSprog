using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> GetByIdAsync(string id);
    Task<int> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto);
    Task<IEnumerable<User>> GetAttendanceAsync(string id);
    Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto);
    Task UpdateLessonAsync(LessonUpdateDTO lessonUpdateDto);
    Task<bool> DeleteAsync(string lessonId);
}