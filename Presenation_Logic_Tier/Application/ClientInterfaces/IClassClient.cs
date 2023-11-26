using Domain.Models;

namespace Application.ClientInterfaces;

public interface IClassClient
{
    Task<ClassEntity> GetByIdAsync(string id);

    Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username);
}