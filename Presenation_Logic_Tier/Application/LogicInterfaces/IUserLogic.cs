using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> AuthenticateUser(string username, string password);
}