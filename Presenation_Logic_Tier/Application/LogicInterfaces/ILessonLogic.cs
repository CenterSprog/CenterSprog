using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> GetByIdAsync(string id);
    Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId);
    Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto);
    
   // Task<Lesson> CreateAsync( LessonCreationDTO lessonCreationDto);
    
    
    //Task UpdateAsync(LessonUpdateDTO updateDto);
    
    Task <bool> DeleteAsync(string lessonId);
    
}