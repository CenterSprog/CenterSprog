using System.Text.RegularExpressions;
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
        ValidateCredentials(username, password);
        User authenticatedUser = await _userClient.GetUserByUsernameAsync(username);

        if (authenticatedUser == null)
            throw new Exception("User not found.");

        if (!authenticatedUser.Password.Equals(password))
            throw new Exception("Password mismatch.");

        return await Task.FromResult(authenticatedUser);
    }

    public async Task<User> CreateUserAsync(UserCreationDTO dto)
    {
        ValidateUserCreation(dto);
        return await _userClient.CreateUserAsync(dto);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userClient.GetUserByUsernameAsync(username);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userClient.GetAllAsync();
    }

    public void ValidateCredentials(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username is required.");
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("Password is required.");
    }

    public void ValidateUserCreation(UserCreationDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            throw new Exception("First name is required.");
        if (string.IsNullOrWhiteSpace(dto.LastName))
            throw new Exception("Last name is required.");
        if (string.IsNullOrWhiteSpace(dto.Role))
            throw new Exception("Role is required.");
        ValidateEmail(dto.Email);
    }

    //     ^: Asserts the start of the string.
    //     [a-zA-Z0-9._-]+: Matches one or more characters that are either letters (both uppercase and lowercase), digits, dots (periods), underscores, or hyphens. This represents the username part of the email.
    //     @: Matches the at symbol, which is a required character in an email address.
    //     [a-zA-Z0-9.-]+: Matches one or more characters that are either letters (both uppercase and lowercase), digits, dots (periods), or hyphens. This represents the domain name part of the email.
    //     \.: Escapes the dot (period) to match it literally. The dot is a special character in regular expressions, so it needs to be escaped to represent an actual dot.
    //     [a-zA-Z]{2,4}: Matches two to four characters that are either letters (both uppercase and lowercase). This represents the top-level domain (TLD) part of the email.
    //     $: Asserts the end of the string.
    public void ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

        if (!Regex.IsMatch(email, emailPattern))
            throw new Exception("Invalid email format. Please use format name@example.com");
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email is required");
    }
}