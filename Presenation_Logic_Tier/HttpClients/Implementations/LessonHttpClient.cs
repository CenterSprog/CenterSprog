using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs.LessonDTO;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.Extensions.Logging;

namespace HttpClients.Implementations;

public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;
    private readonly ILogger<LessonHttpClient> logger;

    public LessonHttpClient(HttpClient client, ILogger<LessonHttpClient> logger)
    {
        this.client = client;
        this.logger = logger;
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

    public async Task<string> MarkAttendanceAsync(MarkAttendanceDTO markAttendanceDto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/lessons/{markAttendanceDto.LessonId}/attendance",
            markAttendanceDto.StudentUsernames);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode || result is null)
        {
            throw new Exception("Failed to mark attendance from blazor");
        }

        return result;
    }

    public async Task<IEnumerable<User>> GetAttendanceAsync(string id)
    {
        HttpResponseMessage responseMessage = await client.GetAsync($"/lessons/{id}/attendance");

        string responseBody = await responseMessage.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(responseBody))
        {
            throw new Exception("Empty response received while fetching attendees.");
        }

        ICollection<User> attendees = JsonSerializer.Deserialize<ICollection<User>>(responseBody,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

        return attendees;
    }


    /*
    public async Task<Lesson> CreateAsync(LessonCreationDTO lessonCreationDto)
    {
      try
      {
        // Convert lessonCreationDto to JSON to include in the request body
        string jsonBody = JsonSerializer.Serialize(lessonCreationDto);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        // Make an asynchronous HTTP POST request
        HttpResponseMessage response = await client.PostAsync("lessons", content);

        // Check if the request was not successful
        if (!response.IsSuccessStatusCode)
        {
            // Log an error and throw an exception
            logger.LogError($"Failed to create lesson. Status code: {response.StatusCode}");
            throw new Exception($"Failed to create lesson. Status code: {response.StatusCode}");
        }

        // Read the response content
        var result = await response.Content.ReadAsStringAsync();

        // Deserialize the result into a Lesson
        var createdLesson = JsonSerializer.Deserialize<Lesson>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        // Check if the deserialization was unsuccessful
        if (createdLesson == null)
        {
            // Log a warning and throw an exception
            logger.LogWarning($"Failed to create lesson. Empty response.");
            throw new Exception($"Failed to create lesson. Empty response.");
        }

        // Return the created lesson
        return createdLesson;
      }
      catch (HttpRequestException ex)
      {
        // Log an error for HTTP request exception and throw an exception
        logger.LogError($"HTTP request error: {ex.Message}");
        throw new Exception($"Failed to create lesson. HTTP request error: {ex.Message}");
      }
      catch (Exception ex)
      {
        // Log an error for other exceptions and throw an exception
        logger.LogError($"An error occurred: {ex.Message}");
        throw new Exception($"Failed to create lesson. An error occurred: {ex.Message}");
      }
    }
       
  */


/*
     public async Task UpdateAsync(LessonUpdateDTO updateDto)
     {
        
         
             // Convert updateDto to JSON to include in the request body
             string jsonBody = JsonSerializer.Serialize(updateDto);
             var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

             // Make an asynchronous HTTP PUT request
             HttpResponseMessage response = await client.PutAsync($"lessons/{updateDto.Id}", content);

             
         
     }*/


    public async Task DeleteAsync(string lessonId)
    {
        try
        {
            HttpResponseMessage response = await client.DeleteAsync($"lessons/{lessonId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}