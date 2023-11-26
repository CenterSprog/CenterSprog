using System.Runtime.CompilerServices;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
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

    public async Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username)
    {
        return await _classClient.GetByUsernameAsync(username);
    }
}