using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ShessBord.Clients;

public abstract class ApiClientBase(HttpClient httpClient, string baseRoute)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _baseRoute = baseRoute.TrimEnd('/');

    protected async Task<T> GetAsync<T>(string endpoint,object? data, string? jwtToken = null)
    {
        var fullUrl = $"{_baseRoute}/{endpoint}";
        var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Bearer", jwtToken);
        }
        
        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }

    protected async Task<T> PostAsync<T>(string endpoint, object? data, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseRoute}/{endpoint}");

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        Console.WriteLine(request);

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }
    
    protected async Task<T> PutAsync<T>(string endpoint, object? data, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseRoute}/{endpoint}");

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }
    
    protected async Task<T> DeleteAsync<T>(string endpoint, object? data, string? jwtToken = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseRoute}/{endpoint}");

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Authorization = 
                new AuthenticationHeaderValue("Bearer", jwtToken);
        }
        
        if (data != null)
        {
            request.Content = JsonContent.Create(data);
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<T>(response);
    }

    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error);
        }

        return (await response.Content.ReadFromJsonAsync<T>())!;
    }
}