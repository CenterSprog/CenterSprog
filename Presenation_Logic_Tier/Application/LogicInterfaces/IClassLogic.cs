using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IClassLogic
{
    Task<ClassEntity> GetByIdAsync(string id);
    Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto);

    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);
    Task<bool> UpdateAsync(ClassUpdateDTO dto);
}