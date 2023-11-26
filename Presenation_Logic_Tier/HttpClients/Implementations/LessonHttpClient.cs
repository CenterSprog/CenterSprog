using System.Net.Http.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.Extensions.Logging;

namespace HttpClients.Implementations;

public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;
    private readonly ILogger<LessonHttpClient> logger;

    public LessonHttpClient(HttpClient client,  ILogger<LessonHttpClient> logger)
    {
        this.client = client;
        this.logger = logger;
    }
    
    public async Task<Lesson> GetByIdAsync(string id)
    {
        HttpResponseMessage response = await client.GetAsync($"/lessons/{id}");
        
        Lesson? foundLesson = await response.Content.ReadFromJsonAsync<Lesson>();
        if (!response.IsSuccessStatusCode || foundLesson is null)
        {
            throw new Exception("Failed to find a lesson with ID#" + id);
        }
        
        return foundLesson;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId)
    {
        HttpResponseMessage response = await client.GetAsync($"/lessons/Class/{classId}");
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"Failed to retrieve lessons for class ID: {classId}. Status code: {response.StatusCode}");
            throw new Exception($"Failed to get lessons for class with ID: {classId}. Status code: {response.StatusCode}");
        }
        IEnumerable<Lesson>? foundLessons = await response.Content.ReadFromJsonAsync<IEnumerable<Lesson>>();
        if (foundLessons == null)
        {
            logger.LogWarning($"Empty response for class ID: {classId}");
            throw new Exception($"Failed to get lessons for class with ID: {classId}. Empty response.");
        }

        return foundLessons;

    }
}