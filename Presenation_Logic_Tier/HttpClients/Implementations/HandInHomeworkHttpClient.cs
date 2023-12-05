using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class HandInHomeworkHttpClient : IHandInHomeworkService
{
    private readonly HttpClient _client;

    public HandInHomeworkHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<HandInHomework> HandInHomework(HomeworkHandInDTO dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/handIns", dto);
        Console.WriteLine($"Response status code: {response.StatusCode}");
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response content: {responseContent}");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to hand in homework. Status code: {response.StatusCode}");
        }

        HandInHomework? handedInHomework = await response.Content.ReadFromJsonAsync<HandInHomework>();
        if (handedInHomework is null)
        {
            throw new Exception("Received invalid homework data from the server.");
        }
         

        return handedInHomework;
    }

    public async Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId)
    {
        HttpResponseMessage response = await _client.GetAsync($"handIns/{homeworkId}");
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get hand ins for homework with ID: {homeworkId}. Status code: {response.StatusCode}");
        }

        List<HandInHomework> foundHandIns = JsonSerializer.Deserialize<List<HandInHomework>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;

        if (foundHandIns == null)
        {
            throw new Exception($"Failed to get hand ins for homework with ID: {homeworkId}. Empty response.");
        }

        return foundHandIns;
    }
}