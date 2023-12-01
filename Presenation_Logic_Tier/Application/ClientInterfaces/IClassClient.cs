﻿using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace Application.ClientInterfaces;

public interface IClassClient
{
    Task<ClassEntity> GetByIdAsync(string id);

    Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto);

    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);

}