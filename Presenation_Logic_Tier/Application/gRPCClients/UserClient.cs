using Application.ClientInterfaces;
using Domain.Models;
using Grpc.Net.Client; 
using gRPCClient;

namespace Application.gRPCClients;

public class UserClient : IUserClient
{
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new UserService.UserServiceClient(channel);
        
        var request = new RequestUserGetByUsername
        {
            Username = username
        };
        Console.WriteLine("LOGGING IN....");
        var reply = new ResponseUserGetByUsername();
        try
        {
            reply = await client.getUserByUsernameAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        User? exisingUser = new User(
            reply.User.Username,
            reply.User.Password,
            reply.User.FirstName,
            reply.User.LastName,
            reply.User.Email,
            reply.User.Role
        );
        return await Task.FromResult(exisingUser);
    }
}