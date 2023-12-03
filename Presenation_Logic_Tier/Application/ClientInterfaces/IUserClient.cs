﻿using Domain.DTOs.UserDTO;
using Domain.Models;

namespace Application.ClientInterfaces;

public interface IUserClient
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> CreateUserAsync(UserCreationDto dto);
    Task<IEnumerable<User>> GetAllAsync();
}