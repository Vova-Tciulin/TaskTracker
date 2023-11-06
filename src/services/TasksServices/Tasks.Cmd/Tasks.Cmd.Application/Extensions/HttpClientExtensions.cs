using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Tasks.Cmd.Application.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new ApplicationException($"Something went wrong calling the API: {errorContent}");
        }
        
        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}