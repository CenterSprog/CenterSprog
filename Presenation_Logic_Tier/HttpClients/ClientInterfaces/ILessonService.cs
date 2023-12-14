﻿using Domain.DTOs.LessonDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ILessonService
{
    Task<Lesson> GetByIdAsync(string id);
    Task<string> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto, string token);
    Task<IEnumerable<User>> GetAttendanceAsync(string id);
    Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto, string token);
    Task UpdateLessonAsync(LessonUpdateDTO updateDto, string token);
    Task DeleteAsync(string lessonId, string token);
    
}