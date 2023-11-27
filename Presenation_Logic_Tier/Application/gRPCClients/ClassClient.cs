using Application.ClientInterfaces;
using Domain.Models;
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
        try
        {

            reply = client.getClassEntityById(request);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        ClassEntity retrievedClassEntity = new(reply.ClassEntity.Id, reply.ClassEntity.Title, reply.ClassEntity.Room);

        return await Task.FromResult(retrievedClassEntity);
    }

    public async Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new ClassEntityService.ClassEntityServiceClient(channel);
        
        var request = new RequestGetClassEntitiesByUsername
        {
            Username = username
        };
        var reply = await client.getClassEntitiesByUsernameAsync(request);

        var classes = new List<ClassEntity>();
        
        foreach (var classEntity in reply.ClassEntities)
        {
            classes.Add(new ClassEntity(classEntity.Id, classEntity.Title, classEntity.Room));
        }

        return await Task.FromResult(classes);
    }
}