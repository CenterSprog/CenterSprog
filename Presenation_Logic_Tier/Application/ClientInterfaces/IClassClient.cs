﻿using Domain.DTOs.ClassDTO;
using Domain.Models;

namespace Application.ClientInterfaces;

public interface IClassClient
{
    Task<ClassEntity> GetByIdAsync(string id);

    Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto);
    Task<IEnumerable<User>> GetAllAttendeesAsync(string id);
    Task<IEnumerable<User>> GetAllParticipantsAsync(string id);

    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);

    Task<bool> UpdateParticipants(ClassUpdateDTO dto);
}