using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IClassService
{
    Task<ClassEntity> GetByIdAsync(string id);
    Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username);
    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);

}