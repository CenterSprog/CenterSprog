using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ILessonService
{
    Task<Lesson> GetByIdAsync(string id);
    Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId);
    
    Task<Lesson> CreateAsync( LessonCreationDTO lessonCreationDto);
    Task<IEnumerable<Lesson>> GetAsync(SearchLessonParametersDTO searchParameters);
    
    Task UpdateAsync(Lesson updateDto);
    
    Task DeleteAsync(string lessonId);
}