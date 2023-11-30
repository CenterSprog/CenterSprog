using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IHandInHomeworkLogic
{
    Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto);
}