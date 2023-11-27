using Application.ClientInterfaces;
using Domain.Models;
using Grpc.Net.Client;
using gRPCClient;

namespace Application.gRPCClients;

public class UserClient : IUserClient
{
    public Task<User> AuthenticateUser(string username, string password)
    {
        // using var channel = GrpcChannel.ForAddress("http://localhost:1111");
        // var client = new UserService.UserServiceClient(channel);
        throw new NotImplementedException();
    }
}