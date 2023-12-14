using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHandInHomeworkService
{
    Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto, string token);
    Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId, string token);
    Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId, string studentUsername);
}