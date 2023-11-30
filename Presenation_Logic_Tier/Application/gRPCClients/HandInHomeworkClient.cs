using System.Collections.Immutable;
using Application.ClientInterfaces;
using Domain.DTOs.HomeworkDTO;
using Grpc.Core;
using Grpc.Net.Client;
using gRPCClient;
using HandInHomework = Domain.Models.HandInHomework;


namespace Application.gRPCClients;

public class HandInHomeworkClient : IHandInHomeworkClient
{
    public async Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new HandInHomeworkService.HandInHomeworkServiceClient(channel);

            var request = new RequestCreateHandInHomework
            {
                HomeworkId = dto.HomeworkId,
                StudentUsername = dto.StudentUsername,
                HandInHomework = new gRPCClient.HandInHomework
                {
                    Id = dto.HandInHomework.Id,
                    Answer = dto.HandInHomework.Answer
                }
            };
            
            var reply = new ResponseGetHandInHomework();
            try
            {
                reply = client.handInHomework(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            HandInHomework createdHandIn =
                new HandInHomework(reply.HandInHomework.Id, reply.HandInHomework.Answer);

            return await Task.FromResult(createdHandIn);





    }
}