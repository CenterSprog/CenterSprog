using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.HeartbeatDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class HeartbeatHttpClient:IHeartbeatService
{
    private readonly HttpClient client;

    public HeartbeatHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<String> CreateAsync(PulseCreationDTO pulseCreation)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/heartbeat", pulseCreation);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        return result;
    }

    public async Task<int> GetAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/heartbeat");

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return Int32.Parse(content);
    }
}