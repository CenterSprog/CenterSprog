﻿using Domain.Models;

namespace Application.ClientInterfaces;

public interface ILessonClient
{
    Task<Lesson> GetByIdAsync(string id);
}