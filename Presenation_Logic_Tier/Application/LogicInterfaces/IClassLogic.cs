﻿using Domain.Models;

namespace Application.LogicInterfaces;

public interface IClassLogic
{
    Task<ClassEntity> GetByIdAsync(string id);
    Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username);
}