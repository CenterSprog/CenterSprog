using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class LessonLogic : ILessonLogic
{
    private readonly ILessonClient _lessonClient;

    public LessonLogic(ILessonClient lessonClient)
    {
        _lessonClient = lessonClient;
    }
    
    public async Task<Lesson> GetByIdAsync(string id)
    {
        return await _lessonClient.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId)
    {
        return await _lessonClient.GetLessonsByClassIdAsync(classId);
    }
}