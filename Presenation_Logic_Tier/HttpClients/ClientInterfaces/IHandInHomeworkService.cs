using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHandInHomeworkService
{
    Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto);
    Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId);
    Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId, string studentUsername);
}