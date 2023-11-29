using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> GetByIdAsync(string id);
    Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId);
    
    Task<Lesson> CreateAsync( LessonCreationDTO lessonCreationDto);
    Task<IEnumerable<Lesson>> GetAsync(SearchLessonParametersDTO searchParameters);
    
    Task UpdateAsync(Lesson updateDto);
    
    Task <bool> DeleteAsync(string lessonId);
    
}