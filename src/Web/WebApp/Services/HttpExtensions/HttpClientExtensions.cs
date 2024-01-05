using System.Net.Http.Headers;
using System.Text.Json;
using WebApp.Services.ModelDto.Group;

namespace WebApp.Services.HttpExtensions;

/// <summary>
/// методы расширения для httpClient 
/// </summary>
public static class HttpClientExtensions
{
    //Десериализация ответа 
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Something went wrong calling the API: {msg}");
        }
        
        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    //Сериализация данных post запросов 
    public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return await httpClient.PostAsync(url, content);
    }
    
    
}