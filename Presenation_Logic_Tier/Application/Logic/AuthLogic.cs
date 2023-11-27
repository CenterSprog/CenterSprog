using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class AuthLogic : IAuthLogic
{
    private AuthClient _authClient;

    public AuthLogic(AuthClient authClient)
    {
        _authClient = authClient;
    }

    public async Task<User> AuthenticateUser(string username, string password)
    {
        User? authenticatedUser = await _authClient.AuthenticateUser(username, password);

        if (authenticatedUser == null)
        {
            throw new Exception("User not found -> (Incorrect login credentials)");
        }

        if (!authenticatedUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch -> (Incorrect login credentials)");
        }

        return await Task.FromResult(authenticatedUser);
    }
    
    
}