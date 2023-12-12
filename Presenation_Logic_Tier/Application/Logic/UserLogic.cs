using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.UserDTO;
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

    public async Task<User> CreateUserAsync(UserCreationDTO dto)
    {
        // Validate email regex
        User? createdUser = await _userClient.CreateUserAsync(dto);
        return await Task.FromResult(createdUser);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        User existingUser = await _userClient.GetUserByUsernameAsync(username);
        return await Task.FromResult(existingUser);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        IEnumerable<User> users = await _userClient.GetAllAsync();
        return await Task.FromResult(users);
    }
}