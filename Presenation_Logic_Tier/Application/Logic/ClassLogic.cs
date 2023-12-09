using System.Runtime.CompilerServices;
using Application.ClientInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs.ClassDTO;
using Domain.DTOs.LessonDTO;
using Domain.DTOs.UserDTO;
using Domain.Models;

namespace Application.Logic;

public class ClassLogic : IClassLogic
{
    private readonly IClassClient _classClient;

    public ClassLogic(IClassClient classClient)
    {
        _classClient = classClient;
    }

    public async Task<ClassEntity> GetByIdAsync(string id)
    {
        return await _classClient.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto)
    {
        return await _classClient.GetAllAsync(dto);
    }


    public async Task<IEnumerable<User>> GetAllParticipantsAsync(SearchClassParticipantsDTO dto)
    {
        return await _classClient.GetAllParticipantsAsync(dto);
    }

    public async Task<ClassEntity> CreateAsync(ClassCreationDTO dto)
    {
        try
        {
            ClassEntity createdClass = await _classClient.CreateAsync(dto);
            return await Task.FromResult(createdClass);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create class entity {e.Message}");
            // SOME ERROR HANDLING ???      
            return await Task.FromException<ClassEntity>(null);
        }
    }

    public async Task<bool> UpdateAsync(ClassUpdateDTO dto)
    {   
        //here in the logic in the future you may want to update the class based on other params like id, title, room or participants
        if (dto.Participants != null)
        {
            bool result = await _classClient.UpdateParticipants(dto);
            return await Task.FromResult(result);

        }
        else
        {
            throw new Exception("It's me Damian:) .Endpoint doesnt server path with given requests data");
        }
    }

    public async Task<IEnumerable<LessonAttendanceDTO>> GetClassAttendanceByUsernameAsync(SearchClassAttendanceDTO dto)
    {
        var lessonsAttended = await _classClient.GetClassAttendanceByUsernameAsync(dto);
        var lessons = (await _classClient.GetByIdAsync(dto.Id)).Lessons; // replace with get lessons?
        var lessonAttendance = lessons
            .Select(lesson => new LessonAttendanceDTO
            {
                Topic = lesson.Topic,
                Date = lesson.Date,
                HasAttended = lessonsAttended.Any(attendedLesson => attendedLesson.Id == lesson.Id)
            });
        return lessonAttendance;
    }

    public async Task<IEnumerable<UserAttendanceDTO>> GetClassAttendanceAsync(string id)
    {
        var participants = await _classClient.GetAllParticipantsAsync(new SearchClassParticipantsDTO(id, "student"));
        var lessonAttendance = await _classClient.GetClassAttendanceAsync(id);
        var lessons = (await _classClient.GetByIdAsync(id)).Lessons; // get lessons
        var participantsWithAttendance = new List<UserAttendanceDTO>();

        foreach (var participant in participants)
        {
            var userAttendanceDto = new UserAttendanceDTO
            {
                FirstName = participant.FirstName,
                LastName = participant.LastName, 
                Email = participant.Email
            };

            var totalLessons = lessons.Count;
            var attendedLessons = lessonAttendance
                .Count(lesson => lesson.Attendees.Any(attendance => attendance.Username == participant.Username));

            var lessonsMissed = totalLessons - attendedLessons;
            var totalAbsence = totalLessons == 0 ? 0 : (double)lessonsMissed / totalLessons * 100;

            userAttendanceDto.TotalAbsence = totalAbsence;
            
            participantsWithAttendance.Add(userAttendanceDto);
        }

        return participantsWithAttendance;
    }
}