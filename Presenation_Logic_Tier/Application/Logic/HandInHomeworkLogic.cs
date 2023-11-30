using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;

namespace Application.Logic;

public class HandInHomeworkLogic : IHandInHomeworkLogic
{
    private readonly HandInHomeworkClient _handInHomeworkClient;

    public HandInHomeworkLogic(HandInHomeworkClient handInHomeworkClient)
    {
        _handInHomeworkClient = handInHomeworkClient;
    }

    public async Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto)
    {

        HandInHomework homeworkToHandIn = await _handInHomeworkClient.HandInHomework(dto);
        return await Task.FromResult(homeworkToHandIn);

    }
}