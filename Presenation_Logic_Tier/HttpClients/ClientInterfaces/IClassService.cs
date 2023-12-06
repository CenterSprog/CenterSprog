using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IClassService
{
    Task<ClassEntity> GetByIdAsync(string id);
    Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto);
    Task<IEnumerable<User>> GetAllAttendeesAsync(string id);

    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);
    Task<Boolean> UpdateClass(string jwt, ClassUpdateDTO dto);
}