using Application.ClientInterfaces;
using Application.gRPCClients;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserClient _userClient;
    
    public UserLogic(IUserClient userClient)
    {
        _userClient = userClient;
    }

    public async Task<User> AuthenticateUserAsync(string username, string password)
    {
        
        User? authenticatedUser = await _userClient.GetUserByUsernameAsync(username);
        
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