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

        HandInHomework handInHomework = JsonSerializer.Deserialize<HandInHomework>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return handInHomework;
    }

    public async Task<IEnumerable<HandInHomework>> GetHandInsByHomeworkIdAsync(string homeworkId)
    {
        HttpResponseMessage response = await _client.GetAsync($"homeworks/{homeworkId}/handIns");
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<HandInHomework> foundHandIns = JsonSerializer.Deserialize<ICollection<HandInHomework>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;

        return foundHandIns;
    }

    public async Task<HandInHomework> GetHandInByHomeworkIdAndStudentUsernameAsync(string homeworkId, string studentUsername)
    {
        HttpResponseMessage response = await _client.GetAsync($"homeworks/{homeworkId}/handIn?username={studentUsername}");
        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        HandInHomework? handIn = JsonSerializer.Deserialize<HandInHomework>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        
        return handIn;
    }
}