﻿using System.Net.Http.Json;
using System.Text.Json;
using Domain.Models;
using HttpClients.ClientInterfaces;


namespace HttpClients.Implementations;

public class ClassHttpClient : IClassService
{
    private readonly HttpClient _client;

    public ClassHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ClassEntity> GetByIdAsync(string id)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/class/{id}");

        ClassEntity? foundClass = await responseMessage.Content.ReadFromJsonAsync<ClassEntity>();
        if (!responseMessage.IsSuccessStatusCode || foundClass is null)
        {
            throw new Exception($"Failed to find a class with this ID + {id}");
        }

        return foundClass;

    }

    public async Task<IEnumerable<ClassEntity>> GetByUsernameAsync(string username)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/class/byUsername/{username}" );
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch classes for username '{username}'. Status code: {responseMessage.StatusCode}");
        }
        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching classes.");
        }
        ICollection<ClassEntity> classes = JsonSerializer.Deserialize<ICollection<ClassEntity>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    
        return classes;
 
        
    }
}