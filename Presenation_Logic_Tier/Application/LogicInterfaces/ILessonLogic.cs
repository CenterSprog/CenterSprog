using Domain.Models;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> GetByIdAsync(string id);

}