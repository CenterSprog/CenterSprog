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
        ValidateHomeworkCreation(dto);
        Homework createdHomework = await _homeworkClient.CreateAsync(dto);
        return await Task.FromResult(createdHomework);
    }

    public void ValidateHomeworkCreation(HomeworkCreationDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.LessonId))
            throw new Exception("Lesson Id is required.");
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new Exception("Title is required.");
        if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Length < 10)
            throw new Exception("Description must be at least 10 characters long");
    }
}