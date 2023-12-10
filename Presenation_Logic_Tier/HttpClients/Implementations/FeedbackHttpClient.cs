using System.Net;
using System.Net.Http.Json;
using Domain.DTOs.FeedbackDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class FeedbackHttpClient : IFeedbackService
{
    private readonly HttpClient _client;

    public FeedbackHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<Feedback> AddFeedbackAsync(AddFeedbackDTO addFeedbackDto)
    {
        HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/feedbacks", addFeedbackDto);
        Feedback? createdFeedback = await responseMessage.Content.ReadFromJsonAsync<Feedback>();
        if (!responseMessage.IsSuccessStatusCode || createdFeedback is null)
        {
            throw new Exception("Failed to give feedback from blazer");
        }

        return createdFeedback;
    }

    public async Task<Feedback> GetFeedbackByHandInIdAndStudentUsernameAsync(string handInId, string studentUsername)
    {
        HttpResponseMessage responseMessage = await _client.GetAsync($"/feedbacks/{handInId}/{studentUsername}");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get feedback. Status code: {responseMessage.StatusCode}");
        }
        
        Feedback? createdFeedback = await responseMessage.Content.ReadFromJsonAsync<Feedback>();
        
        if (!responseMessage.IsSuccessStatusCode || createdFeedback is null)
        {
            throw new Exception("Failed to give feedback from blazer");
        }

        return createdFeedback;
    }
}