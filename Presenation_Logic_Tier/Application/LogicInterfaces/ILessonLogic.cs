using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> GetByIdAsync(string id);
    Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto);
    Task<IEnumerable<User>> GetAttendanceAsync(string id);

   // Task<Lesson> CreateAsync( LessonCreationDTO lessonCreationDto);
    
    
    //Task UpdateAsync(LessonUpdateDTO updateDto);
    
    Task <bool> DeleteAsync(string lessonId);
    
}