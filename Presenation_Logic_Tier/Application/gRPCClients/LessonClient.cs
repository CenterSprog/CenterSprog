using Application.ClientInterfaces;
using Grpc.Net.Client;
using gRPCClient;
using Homework = Domain.Models.Homework;
using Lesson = Domain.Models.Lesson;

namespace Application.gRPCClients;

public class LessonClient : ILessonClient
{
    public async Task<Lesson> GetByIdAsync(string id)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
                
        var request = new RequestGetLessonById()
        {
            LessonId = id
        };
                
        var reply = new ResponseGetLessonById();
        try
        {
            reply = client.getLessonById(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Lesson foundLesson;
        if (reply.Lesson.Homework!=null)
        {
            foundLesson = new(reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic,
                new Homework(reply.Lesson.Homework.Id, reply.Lesson.Homework.Deadline,
                    reply.Lesson.Homework.Title, reply.Lesson.Homework.Description));
        }
        else{
            foundLesson = new(reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic);
        }
                
        return await Task.FromResult(foundLesson);
    }
}