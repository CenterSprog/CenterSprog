using System.Net.Http.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;

    public LessonHttpClient(HttpClient client)
    {
        this.client = client;
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
}