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

    public async Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
        
        var request = new RequestGetLessonsByClassId()
        {
            ClassId = classId
        };
        
        var reply = new ResponseGetLessonsByClassId();
        try
        {
            reply = client.getLessonsByClassId(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        var lessons = new List<Lesson>();
        foreach (var grpcLesson in reply.Lessons)
        {
            if (grpcLesson.Homework != null)
            {
                var homework = new Homework(
                    grpcLesson.Homework.Id,
                    grpcLesson.Homework.Deadline,
                    grpcLesson.Homework.Title,
                    grpcLesson.Homework.Description
                );

                var lesson = new Lesson(
                    grpcLesson.Date,
                    grpcLesson.Description,
                    grpcLesson.Topic,
                    homework
                );

                lessons.Add(lesson);
            }
            else
            {
                var lesson = new Lesson(
                    grpcLesson.Date,
                    grpcLesson.Description,
                    grpcLesson.Topic
                );

                lessons.Add(lesson);
            }
        }

        return await Task.FromResult(lessons);
        
    }
}