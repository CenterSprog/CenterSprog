using System.Net.Http.Json;
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
        HttpResponseMessage response = await _client.PostAsJsonAsync("/handInHomework", dto);
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
}