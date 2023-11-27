using Domain.Models;

namespace Application.ClientInterfaces;

public interface IUserClient
{
    Task<User> AuthenticateUser(string username, string password);
}