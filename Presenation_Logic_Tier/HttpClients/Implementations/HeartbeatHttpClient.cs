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

    public async Task<Heartbeat> Create(PulseCreationDTO pulseCreation)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/heartbeat", pulseCreation);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Heartbeat heartbeat = JsonSerializer.Deserialize<Heartbeat>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        })!;
        return heartbeat;
    }

    public async Task<IEnumerable<Heartbeat>> Get()
    {
        HttpResponseMessage response = await client.GetAsync("/heartbeat");

        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        IEnumerable<Heartbeat> heartbeats = JsonSerializer.Deserialize<IEnumerable<Heartbeat>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        })!;

        return heartbeats;
    }
}