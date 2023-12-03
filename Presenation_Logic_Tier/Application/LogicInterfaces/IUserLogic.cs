using Domain.DTOs.UserDTO;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> AuthenticateUserAsync(string username, string password);
    Task<User> CreateUserAsync(UserCreationDto dto);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllAsync();
}