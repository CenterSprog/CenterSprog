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
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Lesson foundLesson = JsonSerializer.Deserialize<Lesson>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return foundLesson;
    }

    public async Task<string> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/lessons/{markAttendanceDto.LessonId}/attendance",
            markAttendanceDto.StudentUsernames);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode || result is null)
        {
            throw new Exception("Failed to mark attendance from blazor");
        }

        return result;
    }

    public async Task<IEnumerable<User>> GetAttendanceAsync(string id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"/lessons/{id}/attendance");

        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching attendees.");
        }

        ICollection<User> attendees = JsonSerializer.Deserialize<ICollection<User>>(responseBody,
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

        HttpResponseMessage response = await client.PatchAsJsonAsync("/lessons", lessonUpdateDto);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update lesson ");
        }
    }

    public async Task DeleteAsync(string lessonId)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync($"lessons/{lessonId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}