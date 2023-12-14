﻿using System.Text;
using System.Text.RegularExpressions;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.UserDTO;
using Domain.Models;

namespace Application.Logic;

/**
* Class: UserLogic
* Purpose: Class used to handle the logic of the user
* Methods:
*   AuthenticateUserAsync(string username, string password) -> Task<User>
*   CreateUserAsync(UserCreationDTO dto) -> Task<User>
*   GenerateUserCreationCredentials(string firstName,int passwordLength) -> Tuple<string, string>
*   GetUserByUsernameAsync(string username) -> Task<User>
*   GetAllAsync() -> Task<IEnumerable<User>>
*   ValidateCredentials(string username, string password) -> void
*   ValidateUserCreation(UserCreationDTO dto) -> void
*   ValidateEmail(string email) -> void
*/

public class UserLogic : IUserLogic
{
    private readonly IUserClient _userClient;

    /**
    * Purpose: Constructor of the class
    * Arguments:
    *   IUserClient userClient -> Client used to handle the user requests
    */
    public UserLogic(IUserClient userClient)
    {
        _userClient = userClient;
    }

    /**
    * Purpose: Method used to authenticate a user
    * Arguments:
    *   string username -> Username of the user
    *   string password -> Password of the user
    * Return:
    *   Task<User> -> User object
    */

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

    /**
    * Purpose: Method used to create a user
    * Arguments:
    *   UserCreationDTO dto -> DTO used to create a user
    * Return:
    *   Task<User> -> User object
    */

    public async Task<User> CreateUserAsync(UserCreationDTO dto)
    {
        ValidateUserCreation(dto);
        Tuple<string, string> userCreationCredentials = GenerateUserCreationCredentials(dto.FirstName, 8);

        return await _userClient.CreateUserAsync(dto, userCreationCredentials.Item1, userCreationCredentials.Item2);
    }

    /**
    * Purpose: Method used to generate user creation credentials
    * Arguments:
    *   string firstName -> First name of the user
    *   int passwordLength -> Length of the password
    * Return:
    *   Tuple<string, string> -> Tuple of strings
    */

    public Tuple<string, string> GenerateUserCreationCredentials(string firstName, int passwordLength)
    {

        string username = $"{firstName}{GenerateRandomPassword(2)}";
        string password = GenerateRandomPassword(passwordLength);

        return new Tuple<string, string>(username, password);
    }



    /**
    * Purpose: Method used to get a user by username
    * Arguments:
    *   string username -> Username of the user
    * Return:
    *   Task<User> -> User object
    */

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _userClient.GetUserByUsernameAsync(username);
    }

    /**
    * Purpose: Method used to get all users
    * Arguments:
    *   void -> void
    * Return:
    *   Task<IEnumerable<User>> -> List of User objects
    */

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userClient.GetAllAsync();
    }

    /**
    * Purpose: Method used to validate the credentials of a user
    * Arguments:
    *   string username -> Username of the user
    *   string password -> Password of the user
    * Return:
    *   void -> void
    */

    public void ValidateCredentials(string username, string password)
    {

        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username is required.");
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("Password is required.");

    }

    /**
    * Purpose: Method used to validate the creation of a user
    * Arguments:
    *   UserCreationDTO dto -> DTO used to create a user
    * Return:
    *   void -> void
    */

    public void ValidateUserCreation(UserCreationDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            throw new Exception("First name is required.");
        if (string.IsNullOrWhiteSpace(dto.LastName))
            throw new Exception("Last name is required.");
        if (string.IsNullOrWhiteSpace(dto.Role))
            throw new Exception("Role is required.");
        if (!dto.FirstName.Trim().Equals(dto.FirstName))
            throw new Exception("Incorrectly formatted first name. Forbidden spaces on edges.");
        if (!dto.LastName.Trim().Equals(dto.LastName))
            throw new Exception("Incorrectly formatted last name. Forbidden spaces on edges.");
        ValidateEmail(dto.Email);
    }

    /**
    * Purpose: Method used to validate the email of a user
    *     ^: Asserts the start of the string.
    *     [a-zA-Z0-9._-]+: Matches one or more characters that are either letters (both uppercase and lowercase), digits, dots (periods), underscores, or hyphens. This represents the username part of the email.
    *     @: Matches the at symbol, which is a required character in an email address./n\n
    *     [a-zA-Z0-9.-]+: Matches one or more characters that are either letters (both uppercase and lowercase), digits, dots (periods), or hyphens. This represents the domain name part of the email.
    *     \.: Escapes the dot (period) to match it literally. The dot is a special character in regular expressions, so it needs to be escaped to represent an actual dot.
    *     [a-zA-Z]{2,4}: Matches two to four characters that are either letters (both uppercase and lowercase). This represents the top-level domain (TLD) part of the email.
    *     $: Asserts the end of the string.
    * Arguments:
    *   string email -> Email of the user
    * Return:
    *   void -> void
    */

    public void ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

        if (!Regex.IsMatch(email, emailPattern))
            throw new Exception("Invalid email format. Please use format name@example.com");
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email is required");
    }

    /**
    * Purpose: Method used to generate a random password
    * Description: Creates a password that contains uppercase and lowercase letters, numbers and special characters
    * Arguments:
    *   int length -> Length of the password
    * Return:
    *   string -> String
    */
    static string GenerateRandomPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
        StringBuilder password = new StringBuilder();

        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(chars.Length);
            password.Append(chars[index]);
        }

        return password.ToString();
    }
}