using Application.ClientInterfaces;
using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Logic;

public class HomeworkLogic : IHomeworkLogic
{
    private readonly IHomeworkClient _homeworkClient;

    public HomeworkLogic(IHomeworkClient homeworkClient)
    {
        _homeworkClient = homeworkClient;
    }

    public async Task<Homework> CreateAsync(HomeworkCreationDTO dto)
    {
        try
        {
            Homework createdHomework = await _homeworkClient.CreateAsync(dto);
            return await Task.FromResult(createdHomework);
        }
        catch (Exception e)
        { 
            Console.WriteLine(e);
            // SOME ERROR HANDLING ???      
            return await Task.FromException<Homework>(null);
        }
    }
}