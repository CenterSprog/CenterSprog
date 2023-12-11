using Application.ClientInterfaces;
using Domain.DTOs.UserDTO;
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

    public async Task<User> CreateUserAsync(UserCreationDTO dto)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new UserService.UserServiceClient(channel);
        var request = new RequestCreateUser
        {
            User = new UserCreationData
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = dto.Role
            }
        };

        var reply = new ResponseCreateUser();
        try
        {
            reply = await client.createUserAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        User? createdUser = new User(
            reply.User.Username,
            reply.User.Password,
            reply.User.FirstName,
            reply.User.LastName,
            reply.User.Email,
            reply.User.Role
        );
        return await Task.FromResult(createdUser);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        var client = new UserService.UserServiceClient(channel);
        var request = new Google.Protobuf.WellKnownTypes.Empty();
        var reply = new ResponseUserGetAllUsers();

        try
        {
            reply = await client.getAllUsersAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        List<User> retrivedUsers = new List<User>();
        foreach (var userData in reply.Users)
        {
            retrivedUsers.Add(new User(
                userData.Username,userData.Password,userData.FirstName,userData.LastName,userData.Email,userData.Role));
        }
        return await Task.FromResult(retrivedUsers);
    }
}