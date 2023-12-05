using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ILessonService
{
    Task<Lesson> GetByIdAsync(string id);
    
    //Task<Lesson> CreateAsync( LessonCreationDTO lessonCreationDto);
    
    
    //Task UpdateAsync(LessonUpdateDTO updateDto);
    
    Task DeleteAsync(string lessonId);
}