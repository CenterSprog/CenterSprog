﻿using Application.ClientInterfaces;
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

        return await Task.FromResult(foundLesson);
    }

    public async Task<int> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);
        //create methods in proto file create response methods and create body 

        var request = new RequestMarkAttendance()
        {
            LessonId = markAttendanceDto.LessonId,
            Usernames = {markAttendanceDto.StudentUsernames}
        };
        var reply = new ResponseMarkAttendance();
        try
        {
            reply = await client.markAttendanceAsync(request);
        } catch (RpcException e)
        {

            Console.WriteLine($" gRPC call failed: {e.Status}");
            throw;
        }

        return await Task.FromResult(reply.AmountOfParticipants);
    }

    public async Task<IEnumerable<User>> GetAttendanceAsync(string id)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new LessonService.LessonServiceClient(channel);

        var request = new RequestGetAttendance()
        {
            LessonId = id
        };

        var reply = new ResponseGetAttendance();

        reply = await client.getAttendanceAsync(request);
        
        var attendees = new List<User>();
        
        foreach (var attendee in reply.Attendees)
        {
            attendees.Add(new User
            {
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Username = attendee.Username
            });
        }

        return await Task.FromResult(attendees);
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