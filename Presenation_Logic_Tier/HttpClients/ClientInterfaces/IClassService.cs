using Domain.DTOs.ClassDTO;
using Domain.DTOs.LessonDTO;
using Domain.DTOs.UserDTO;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IClassService
{
    Task<ClassEntity> GetByIdAsync(string id);
    Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto);
    Task<IEnumerable<User>> GetAllParticipantsAsync(SearchClassParticipantsDTO dto);
    Task<ClassEntity> CreateAsync(ClassCreationDTO dto);
    Task<Boolean> UpdateClass(string jwt, ClassUpdateDTO dto);
    Task<IEnumerable<UserAttendanceDTO>> GetClassAttendanceAsync(string id);
    Task<IEnumerable<LessonAttendanceDTO>> GetClassAttendanceByUsernameAsync(SearchClassAttendanceDTO dto);
}