using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.ClientInterfaces;

public interface ILessonClient
{
    Task<Lesson?> GetByIdAsync(string id);
    Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto);
    //Task<Lesson> CreateAsync(Lesson lesson);
    
    
    //Task UpdateAsync(LessonUpdateDTO updateDto);
    Task DeleteAsync(string lessonId);
}