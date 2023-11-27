using Domain.Models;

namespace Application.LogicInterfaces;

public interface IAuthLogic
{
    Task<User> AuthenticateUser(string username, string password);
}