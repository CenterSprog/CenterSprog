using Application.ClientInterfaces;
using Domain.Models;

namespace Application.gRPCClients;

public class AuthClient : IAuthClient
{
    public Task<User> AuthenticateUser(string username, string password)
    {
        throw new NotImplementedException();
    }
}