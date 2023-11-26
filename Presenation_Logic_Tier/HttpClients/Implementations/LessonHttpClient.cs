using System.Net.Http.Json;
using System.Text.Json;
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
}