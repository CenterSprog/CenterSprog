using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> AuthenticateUserAsync(string username, string password);
}