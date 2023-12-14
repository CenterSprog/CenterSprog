﻿using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace Application.Logic;

public class HandInHomeworkLogic : IHandInHomeworkLogic
{
    private readonly IHandInHomeworkClient _handInHomeworkClient;

    public HandInHomeworkLogic(IHandInHomeworkClient handInHomeworkClient)
    {
        _handInHomeworkClient = handInHomeworkClient;
    }

    public async Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto)
    {
        ValidateHandInCreation(dto);
        HandInHomework homeworkToHandIn = await _handInHomeworkClient.HandInHomework(dto);
        return await Task.FromResult(homeworkToHandIn);
    }

    public async Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId)
    {
        return await _handInHomeworkClient.GetHandInsByHomeworkIdAsync(homeworkId);
    }

    public async Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId,
        string studentUsername)
    {
        return await _handInHomeworkClient.GetHandInByHomeworkIdAndStudentUsernameAsync(homeworkId, studentUsername);
    }

    public void ValidateHandInCreation(HomeworkHandInDTO dto)
    {
        if (string.IsNullOrEmpty(dto.StudentUsername))
            throw new Exception("Student Username is required");
        if (string.IsNullOrEmpty(dto.HomeworkId))
            throw new Exception("Homework Id is required");
        if (string.IsNullOrWhiteSpace(dto.Answer))
            throw new Exception("Answer is required.");
    }
}