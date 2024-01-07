using System.Net;
using System.Text.Json;

namespace Groups.Cmd.Application.Extensions;

/// <summary>
/// Методы расширения для httpClient
/// </summary>
public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if(response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            throw new ApplicationException("ошибка авторизации при попытки обратиться к сервису");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new ApplicationException($"{errorContent}");
        }
        
        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}