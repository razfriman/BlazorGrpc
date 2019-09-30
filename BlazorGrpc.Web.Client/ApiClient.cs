using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorGrpc.Protos;
using Google.Protobuf;
using Microsoft.AspNetCore.Components;

namespace BlazorGrpc.Web.Client
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private string _apiUrl;
        private bool _initialized;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetWeatherResponse> GetWeather(GetWeatherRequest request) =>
            await SendMessage<GetWeatherResponse>(request,
                Path.Combine("WeatherService", "GetWeather"), GetWeatherResponse.Parser);

        private async Task<TResponse> SendMessage<TResponse>(object request, string path,
            MessageParser parser) where TResponse : class
        {
            if (!_initialized)
            {
                await LoadConfiguration();
            }

            var httpContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var httpResp = await _httpClient.PostAsync(Path.Combine(_apiUrl, path), httpContent);

            if (httpResp.IsSuccessStatusCode)
            {
                return (TResponse) parser.ParseJson(await httpResp.Content.ReadAsStringAsync());
            }

            return null;
        }

        private async Task LoadConfiguration()
        {
            var result = await _httpClient.GetJsonAsync<WebAppConfiguration>("/configuration");
            _apiUrl = result.ApiUrl;
            _initialized = true;
        }
    }
}
