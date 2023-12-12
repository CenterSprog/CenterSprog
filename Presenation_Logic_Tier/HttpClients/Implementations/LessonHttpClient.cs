using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.LessonDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.Extensions.Logging;

namespace HttpClients.Implementations;

public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;
    private readonly ILogger<LessonHttpClient> logger;

    public LessonHttpClient(HttpClient client, ILogger<LessonHttpClient> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public async Task<Lesson> GetByIdAsync(string id)
    {
        HttpResponseMessage response = await client.GetAsync($"/lessons/{id}");
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        Lesson foundLesson = JsonSerializer.Deserialize<Lesson>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return foundLesson;
    }

    public async Task<string> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/lessons/{markAttendanceDto.LessonId}/attendance",
            markAttendanceDto.StudentUsernames);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return responseContent;
    }

    public async Task<IEnumerable<User>> GetAttendanceAsync(string id)
    {
        HttpResponseMessage response = await client.GetAsync($"/lessons/{id}/attendance");

        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        ICollection<User> attendees = JsonSerializer.Deserialize<ICollection<User>>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return attendees;
    }


    public async Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto)
    {

        HttpResponseMessage response = await client.PostAsJsonAsync("/lessons", lessonCreationDto);
        Lesson? createdLesson = await response.Content.ReadFromJsonAsync<Lesson>();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create a new lesson ");
        }

        return createdLesson;
    }
    public async Task UpdateLessonAsync(LessonUpdateDTO lessonUpdateDto)
    {

        HttpResponseMessage response = await client.PutAsJsonAsync("/lessons", lessonUpdateDto);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task DeleteAsync(string lessonId)
    {
        HttpResponseMessage response = await client.DeleteAsync($"lessons/{lessonId}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }
    }
}