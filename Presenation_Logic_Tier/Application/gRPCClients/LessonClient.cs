using Application.ClientInterfaces;
using Domain.DTOs.LessonDTO;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using gRPCClient;
using Domain.Models;
using Grpc.Core;
using Homework = Domain.Models.Homework;


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
            foundLesson = new(reply.Lesson.Id,reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic,
                new Homework(reply.Lesson.Homework.Id, reply.Lesson.Homework.Deadline,
                    reply.Lesson.Homework.Title, reply.Lesson.Homework.Description));
        }
        else{
            foundLesson = new(reply.Lesson.Id, reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic);
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
                var homework = new Domain.Models.Homework(
                    grpcLesson.Homework.Id,
                    grpcLesson.Homework.Deadline,
                    grpcLesson.Homework.Title,
                    grpcLesson.Homework.Description
                );

                var lesson = new Lesson(
                    grpcLesson.Id,
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
                    grpcLesson.Id,
                    grpcLesson.Date,
                    grpcLesson.Description,
                    grpcLesson.Topic
                );

                lessons.Add(lesson);
            }
        }

        return await Task.FromResult(lessons);

    }

    public Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto)
    {
        throw new NotImplementedException();
    }


    /*
    public async Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto)
    {

        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);

        var request = new RequestAddLesson

        {
            ClassId = lessonCreationDto.classId,
            Lesson = new LessonData
            {
                Topic = lessonCreationDto.Topic,
                Date = lessonCreationDto.Date,
                Description = lessonCreationDto.Description
            }
        };

        var reply = new ResponseAddLesson();
        try
        {
            reply = await client.addLessonAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Lesson createdLesson =
            new Lesson(reply.Lesson.Id, reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic);
        return await Task.FromResult(createdLesson);
    }

    */
    

    public async Task DeleteAsync(string lessonId)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
        //create methods in proto file create response methods and create body 

        var request = new RequestDeleteLesson
        {
            LessonId = lessonId
        };

        try
        {
            var response = client.deleteLesson(request);

            if (lessonId == null)
            {
                throw new Exception($"Lesson with ID {lessonId} was not found!");
            }

           
        } catch (RpcException e)
        {

            Console.WriteLine($" gRPC call failed: {e.Status}");
            throw;
        }
        
    }


/*
    public async Task UpdateAsync(Lesson updateDto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
    }
    */
}