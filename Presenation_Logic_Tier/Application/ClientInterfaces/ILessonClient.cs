using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.ClientInterfaces;

public interface ILessonClient
{
    Task<Lesson> GetByIdAsync(string id);
    Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId);
    Task<Lesson> CreateAsync(Lesson lesson);
    Task<IEnumerable<Lesson>> GetAsync(SearchLessonParametersDTO searchParameters);
    
    Task UpdateAsync(Lesson updateDto);
    Task DeleteAsync(string lessonId);
}