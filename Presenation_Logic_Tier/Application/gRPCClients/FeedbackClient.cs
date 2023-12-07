using Application.ClientInterfaces;
using Domain.DTOs.FeedbackDTO;
using Grpc.Net.Client;
using gRPCClient;
using Feedback = Domain.Models.Feedback;

namespace Application.gRPCClients;

public class FeedbackClient : IFeedbackClient
{
    public async Task<Feedback> AddFeedbackAsync(AddFeedbackDTO addFeedbackDto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new FeedbackService.FeedbackServiceClient(channel);

        var request = new RequestAddFeedback()
        {
            HandInId = addFeedbackDto.HandInId,
            StudentUsername = addFeedbackDto.StudentUsername,
            Feedback = new gRPCClient.Feedback
            {
                Id = addFeedbackDto.Feedback.Id,
                Grade = addFeedbackDto.Feedback.Grade,
                Comment = addFeedbackDto.Feedback.Comment
            }
        };

        var reply = new gRPCClient.Feedback();
        try
        {
            reply = await client.addFeedbackAsync(request);
            Console.WriteLine($"Feedback was successfully added!");
        }
        catch (Grpc.Core.RpcException e) when (e.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            Console.WriteLine($"Feedback already provided for this hand-in.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }

        Feedback createdFeedback = new Feedback(reply.Id, reply.Grade, reply.Comment);
        return await Task.FromResult(createdFeedback);
    }

    public async Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new FeedbackService.FeedbackServiceClient(channel);
            
        var request = new RequestGetFeedbackByHandInIdAndStudentUsername
        {
            HandInId = handInId,
            StudentUsername = studentUsername
        };
            
        try
        {
            var response = await client.getFeedbackByHandInIdAndStudentUsernameAsync(request);
                
            if (response != null && response.Feedback != null)
            {
                var feedback = response.Feedback;
                return new Feedback(feedback.Id, feedback.Grade, feedback.Comment);
            }
            else
            {
                return null; 
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null; 
        }
    }

}