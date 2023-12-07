using Application.ClientInterfaces;
using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace Application.Logic;

public class HandInHomeworkLogic : IHandInHomeworkLogic
{
    private readonly IHandInHomeworkClient _handInHomeworkClient;

    public HandInHomeworkLogic(HandInHomeworkClient handInHomeworkClient)
    {
        _handInHomeworkClient = handInHomeworkClient;
    }

    public async Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto)
    {

        HandInHomework homeworkToHandIn = await _handInHomeworkClient.HandInHomework(dto);
        return await Task.FromResult(homeworkToHandIn);

    }

    public async Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId)
    {
        return await _handInHomeworkClient.GetHandInsByHomeworkIdAsync(homeworkId);
    }

    public async Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId, string studentUsername)
    {
        return await _handInHomeworkClient.GetHandInByHomeworkIdAndStudentUsernameAsync(homeworkId, studentUsername);
    }
}