using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Core.Exceptions;

namespace Clean.Application.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<U> PostAsJsonAsync<T, U>(this HttpClient httpClient, string url, T data, JsonSerializerOptions jsonSerializerOptions = null)
        {
            var dataAsString = JsonSerializer.Serialize(data, jsonSerializerOptions);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJsonAsync<U>();
            }
            else
            {
                throw new FriendlyException(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(dataAsString);
        }
    }
}
