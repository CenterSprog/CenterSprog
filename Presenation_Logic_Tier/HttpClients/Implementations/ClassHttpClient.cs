using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.ClassDTO;
using Domain.DTOs.LessonDTO;
using Domain.DTOs.UserDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;


namespace HttpClients.Implementations;

public class ClassHttpClient : IClassService
{
    private readonly HttpClient _client;

    public ClassHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ClassEntity> GetByIdAsync(string id)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/classes/{id}");

        ClassEntity? foundClass = await responseMessage.Content.ReadFromJsonAsync<ClassEntity>();
        if (!responseMessage.IsSuccessStatusCode || foundClass is null)
        {
            throw new Exception($"Failed to find a class with this ID + {id}");
        }

        return foundClass;
    }

    public async Task<IEnumerable<ClassEntity>> GetAllAsync(SearchClassDTO dto)
    {
        String username = dto.Username;
        String url = "/classes";
        if (username != null)
        {
            url += $"?username={username}";
        }
        HttpResponseMessage responseMessage = await _client.GetAsync(url);

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Failed to fetch classes for username '{username}'. Status code: {responseMessage.StatusCode}");
        }
        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching classes.");
        }

        ICollection<ClassEntity> classes = JsonSerializer.Deserialize<ICollection<ClassEntity>>(responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return classes;
    }

    public async Task<IEnumerable<User>> GetAllParticipantsAsync(SearchClassParticipantsDTO dto)
    {
        string query = "";
        if (!string.IsNullOrEmpty(dto.Role))
           query += $"?role={dto.Role}";
        HttpResponseMessage responseMessage = await _client.GetAsync($"/classes/{dto.Id}/participants"+ query);

        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching participants.");
        }

        ICollection<User> participants = JsonSerializer.Deserialize<ICollection<User>>(responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return participants;
    }

    public async Task<ClassEntity> CreateAsync(ClassCreationDTO dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/classes/", dto);
        ClassEntity? createdClassEntity = await response.Content.ReadFromJsonAsync<ClassEntity>();
        if (!response.IsSuccessStatusCode || createdClassEntity is null)
        {
            throw new Exception("Failed to create a new class from blazer");
        }

        return createdClassEntity;
    }

    public async Task<bool> UpdateClass(string jwt, ClassUpdateDTO dto)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        HttpResponseMessage response = await _client.PatchAsJsonAsync($"/classes", dto);
        Boolean result = await response.Content.ReadFromJsonAsync<Boolean>();
        if (!response.IsSuccessStatusCode || result == false)
        {
            throw new Exception("Failed to updated a class, http blazor client");
        }

        return result;
    }

    public async Task<IEnumerable<UserAttendanceDTO>> GetClassAttendanceAsync(string id)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/classes/{id}/attendances");

        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching attendees.");
        }

        ICollection<UserAttendanceDTO> attendees = JsonSerializer.Deserialize<ICollection<UserAttendanceDTO>>(responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return attendees;
    }

    public async Task<IEnumerable<LessonAttendanceDTO>> GetClassAttendanceByUsernameAsync(SearchClassAttendanceDTO dto)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/classes/{dto.Id}/attendances?username={dto.Username}");

        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching lessons.");
        }

        ICollection<LessonAttendanceDTO> lessons = JsonSerializer.Deserialize<ICollection<LessonAttendanceDTO>>(responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return lessons;
    }
}