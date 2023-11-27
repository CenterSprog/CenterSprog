using Domain.Models;

namespace Application.ClientInterfaces;

public interface IAuthClient
{
    Task<User> AuthenticateUser(string username, string password);
}