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

        reply = await client.getLessonByIdAsync(request);
        
        Lesson foundLesson = new(reply.Lesson.Id,reply.Lesson.Date, reply.Lesson.Description, reply.Lesson.Topic);

        if (reply.Lesson.Homework != null)
            foundLesson.Homework = new Homework(reply.Lesson.Homework.Id, reply.Lesson.Homework.Deadline,
                reply.Lesson.Homework.Title, reply.Lesson.Homework.Description); 
                
        if (reply.Lesson.Attendees.Any())
        {
            foreach (var attendee in reply.Lesson.Attendees)
            {
                foundLesson.Attendees.Add(new User(
                    attendee.Username,
                    attendee.FirstName,
                    attendee.LastName));
            }
        }

        return await Task.FromResult(foundLesson);
    }
    
    public Task<int> AddAttendance(AddAttendanceDTO addAttendanceDto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
        //create methods in proto file create response methods and create body 

        var request = new RequestAddAttendance()
        {
            LessonId = addAttendanceDto.LessonId,
            Usernames = {addAttendanceDto.StudentUsernames}
        };
        var reply = new ResponseAddAttendance();
        try
        {
            reply = client.addAttendance(request);
        } catch (RpcException e)
        {

            Console.WriteLine($" gRPC call failed: {e.Status}");
            throw;
        }

        return Task.FromResult(reply.AmountOfParticipants);
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