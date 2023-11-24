using System.Net.Http.Json;
using Domain.DTOs.HomeworkDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class HomeworkHttpClient: IHomeworkService
{
    private readonly HttpClient _client;

    public HomeworkHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Homework> CreateAsync(HomeworkCreationDTO dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/homeworks", dto);
        Homework? createdHomework = await response.Content.ReadFromJsonAsync<Homework>();
        if (!response.IsSuccessStatusCode || createdHomework is null)
        {
            throw new Exception("Failed to create a new homework from blazer");
        }
        
        return createdHomework;
    }
}