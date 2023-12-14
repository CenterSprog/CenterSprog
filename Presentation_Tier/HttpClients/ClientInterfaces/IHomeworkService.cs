using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IHomeworkService
{
    Task<Homework> CreateAsync(HomeworkCreationDTO dto, string token);
}