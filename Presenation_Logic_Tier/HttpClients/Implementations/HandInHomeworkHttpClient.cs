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
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
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
            throw new Exception(result);
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

    public async Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId, string studentUsername)
    {
        HttpResponseMessage response = await _client.GetAsync($"handIns/{homeworkId}/student/{studentUsername}");
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        HandInHomework? handIn = JsonSerializer.Deserialize<HandInHomework>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (handIn == null)
        {
            throw new Exception($"Failed to deserialize hand-in data for homework ID: {homeworkId} and student username: {studentUsername}.");
        }

        return handIn;
    }
}