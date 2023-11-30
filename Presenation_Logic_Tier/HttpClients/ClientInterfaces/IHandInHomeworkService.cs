using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHandInHomeworkService
{
    Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto);
}