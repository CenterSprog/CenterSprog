using System.Net;
using System.Net.Http.Json;
using System.Text;
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

    public LessonHttpClient(HttpClient client,  ILogger<LessonHttpClient> logger)
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

    public async Task<IEnumerable<Lesson>> GetLessonsByClassIdAsync(string classId)
    {
        HttpResponseMessage response = await client.GetAsync($"lessons/Class/{classId}");
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
        
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"Failed to retrieve lessons for class ID: {classId}. Status code: {response.StatusCode}");
            throw new Exception($"Failed to get lessons for class with ID: {classId}. Status code: {response.StatusCode}");
        }
    
        List<Lesson> foundLessons = JsonSerializer.Deserialize<List<Lesson>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        
        if (foundLessons == null)
        {
            logger.LogWarning($"Empty response for class ID: {classId}");
            throw new Exception($"Failed to get lessons for class with ID: {classId}. Empty response.");
        }

        return foundLessons;

    }

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
       
  
    public async Task<IEnumerable<Lesson>> GetAsync(SearchLessonParametersDTO searchParameters)
    {
        HttpResponseMessage response = await client.GetAsync($"lessons");
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
        
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"Failed to retrieve lessons. Status code: {response.StatusCode}");
            throw new Exception($"Failed to get lessons. Status code: {response.StatusCode}");
        }
    
        List<Lesson> foundLessons = JsonSerializer.Deserialize<List<Lesson>>(result, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        if (foundLessons == null)
        {
            logger.LogWarning($"Failed to get lessons. Empty response.");
            throw new Exception($"Failed to get lessons. Empty response.");
        }

        return foundLessons;
     
        
    }

    public async Task UpdateAsync(Lesson updateDto)
    {
        try
        {
                // Convert updateDto to JSON to include in the request body
                string jsonBody = JsonSerializer.Serialize(updateDto);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Make an asynchronous HTTP PUT request
                HttpResponseMessage response = await client.PutAsync($"lessons/{updateDto.Id}", content);

                // Check if the request was not successful
                if (!response.IsSuccessStatusCode)
                {
                    // Log an error and throw an exception
                    logger.LogError($"Failed to update lesson. Status code: {response.StatusCode}");
                    throw new Exception($"Failed to update lesson. Status code: {response.StatusCode}");
                }

                // If needed, you can read the response content
                var result = await response.Content.ReadAsStringAsync();

                // You might want to handle the response content based on your requirements

        }
        catch (HttpRequestException ex)
        {
                // Log an error for HTTP request exception and throw an exception
                logger.LogError($"HTTP request error: {ex.Message}");
                throw new Exception($"Failed to update lesson. HTTP request error: {ex.Message}");
        }
        catch (Exception ex)
        {
                // Log an error for other exceptions and throw an exception
                logger.LogError($"An error occurred: {ex.Message}");
                throw new Exception($"Failed to update lesson. An error occurred: {ex.Message}");
        }
    }


    public async Task DeleteAsync(string lessonId)
    {
        try
        {
                // Make an asynchronous HTTP DELETE request
                HttpResponseMessage response = await client.DeleteAsync($"lessons/{lessonId}");

                // Check if the request was not successful
                if (!response.IsSuccessStatusCode)
                {
                    // Log an error and throw an exception
                    logger.LogError($"Failed to delete lesson. Status code: {response.StatusCode}");
                    throw new Exception($"Failed to delete lesson. Status code: {response.StatusCode}");
                }

                // If needed, you can read the response content
                // var result = await response.Content.ReadAsStringAsync();

                // You might want to handle the response content based on your requirements

        }
        catch (HttpRequestException ex)
        {
                // Log an error for HTTP request exception and throw an exception
                logger.LogError($"HTTP request error: {ex.Message}");
                throw new Exception($"Failed to delete lesson. HTTP request error: {ex.Message}");
        }
        catch (Exception ex)
        {
                // Log an error for other exceptions and throw an exception
                logger.LogError($"An error occurred: {ex.Message}");
                throw new Exception($"Failed to delete lesson. An error occurred: {ex.Message}");
        }
    }

    
    
}