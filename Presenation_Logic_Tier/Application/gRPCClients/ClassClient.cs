using Application.ClientInterfaces;
using Domain.DTOs.ClassDTO;
using Domain.Models;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using gRPCClient;
using ClassEntity = Domain.Models.ClassEntity;

namespace Application.gRPCClients;

public class ClassClient : IClassClient
{
    public async Task<ClassEntity> GetByIdAsync(string id)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);

        var request = new RequestGetClassEntity()
        {
            ClassId = id
        };

        var reply = new ResponseGetClassEntity();
        
        reply =  await client.getClassEntityByIdAsync(request);


        ClassEntity retrievedClassEntity = new(reply.ClassEntity.Id, reply.ClassEntity.Title, reply.ClassEntity.Room);
        if(reply.ClassEntity.Participants.Any()){
            foreach (var userParticipant in reply.ClassEntity.Participants)
            {
                retrievedClassEntity.Participants.Add(new User(userParticipant.Username, userParticipant.FirstName,
                    userParticipant.LastName, userParticipant.Role, userParticipant.Email));
            }
        }

        if (reply.ClassEntity.Lessons.Any())
        {

            foreach (var lesson in reply.ClassEntity.Lessons)
            {
                retrievedClassEntity.Lessons.Add(new Lesson(lesson.Id, lesson.Date, lesson.Description, lesson.Topic));
            }
        }

        return await Task.FromResult(retrievedClassEntity);
    }

    public async Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);
        var request = new RequestGetClassEntities();
        if (dto.Username != null)
            request.Username = dto.Username;
        
        
        var reply = await client.getClassEntitiesAsync(request);

        var classes = new List<ClassEntity>();
        
        foreach (var classData in reply.ClassEntities)
        {
            ClassEntity newClass = new ClassEntity(classData.Id, classData.Title, classData.Room);

            foreach (var userParticipant in classData.Participants)
            {
                newClass.Participants.Add( new User(userParticipant.Username,userParticipant.FirstName,userParticipant.LastName,userParticipant.Role));
            }
            
            classes.Add(newClass);
        }

        return await Task.FromResult(classes);
    }

    public async Task<IEnumerable<User>> GetAllAttendeesAsync(string id)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);
        var request = new RequestGetClassAttendees
        {
            ClassId = id
        };
        
        var reply = await client.getClassAttendeesAsync(request);

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

    public async Task<ClassEntity> CreateAsync(ClassCreationDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);
        var request = new RequestCreateClassEntity
        {
            ClassEntityCreation = new ClassEntityCreation
            {
                Room = dto.Room,
                Title = dto.Room
            }
        };
        var reply = new ResponseCreateClassEntity();
        try
        {
            reply = await client.createClassEntityAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        ClassEntity createdClass = new ClassEntity(reply.ClassEntity.Id, reply.ClassEntity.Title, reply.ClassEntity.Room);
        return await Task.FromResult(createdClass);
    }

    public async Task<bool> UpdateParticipants(ClassUpdateDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);
        var participantsData = new RepeatedField<string>();
        foreach (string username in dto.Participants)
        {
            participantsData.Add( username );
        }

        var request = new RequestUpdateClassParticipants
        {
            Id = dto.Id,
            ParticipantsUsernames = {  participantsData }
        };
        var reply = new ResponseUpdateClassParticipants();

        try
        {
            reply = await client.updateParticipantsAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + e.StackTrace);
            return await Task.FromResult(false);
        }

        return await Task.FromResult(reply.Result);
    }
}