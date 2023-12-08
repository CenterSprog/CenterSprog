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

    public async Task<IEnumerable<User>> GetAllAttendeesAsync(string id)
    {
        return await _classClient.GetAllAttendeesAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllParticipantsAsync(string id)
    {
        return await _classClient.GetAllParticipantsAsync(id);
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

    public async Task<bool> UpdateAsync(ClassUpdateDTO dto)
    {   
        //here in the logic in the future you may want to update the class based on other params like id, title, room or participants
        if (dto.Participants != null)
        {
            bool result = await _classClient.UpdateParticipants(dto);
            return await Task.FromResult(result);

        }
        else
        {
            throw new Exception("It's me Damian:) .Endpoint doesnt server path with given requests data");
        }
    }
}