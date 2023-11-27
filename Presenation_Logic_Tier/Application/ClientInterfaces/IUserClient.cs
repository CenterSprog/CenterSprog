using Domain.Models;

namespace Application.ClientInterfaces;

public interface IUserClient
{
    Task<User> GetUserByUsernameAsync(string username);
}