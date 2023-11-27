using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly UserClient _userClient;

    public UserLogic(UserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<User> AuthenticateUser(string username, string password)
    {
        User? authenticatedUser = await _userClient.AuthenticateUser(username, password);

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