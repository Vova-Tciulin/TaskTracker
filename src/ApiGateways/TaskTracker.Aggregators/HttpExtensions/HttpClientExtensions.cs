using System.Net;
using System.Text.Json;

namespace TaskTracker.Aggregators.HttpExtensions;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if(response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            throw new ApplicationException("ошибка авторизации при попытки обратиться к сервису");
        }
        
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"{response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}