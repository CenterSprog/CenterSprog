using System.Runtime.CompilerServices;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace Application.Logic;

public class ClassLogic : IClassLogic
{
    private readonly IClassClient _classClient;

    public ClassLogic(IClassClient classClient)
    {
        _classClient = classClient;
    }

    public async Task<ClassEntity> GetByIdAsync(string id)
    {
        return await _classClient.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto)
    {
        return await _classClient.GetAllAsync(dto);
    }

    public async Task<ClassEntity> CreateAsync(ClassCreationDTO dto)
    {
        try
        {
            ClassEntity createdClass = await _classClient.CreateAsync(dto);
            return await Task.FromResult(createdClass);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create class entity {e.Message}");
            // SOME ERROR HANDLING ???      
            return await Task.FromException<ClassEntity>(null);
        }
    }
}