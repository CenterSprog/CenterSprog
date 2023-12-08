using Domain.DTOs.LessonDTO;
using Domain.Models;
using gRPCClient;

namespace Application.ClientInterfaces;

public interface ILessonClient
{
    Task<Lesson?> GetByIdAsync(string id);
    Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto);
    Task<IEnumerable<User>> GetAttendanceAsync(string id);

    //Task<Lesson> CreateAsync(Lesson lesson);
    
    
    //Task UpdateAsync(LessonUpdateDTO updateDto);
    Task DeleteAsync(string lessonId);
}