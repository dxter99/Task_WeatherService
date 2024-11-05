using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherService.Models;

namespace WeatherService.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public WeatherDataService(HttpClient httpClient, IOptions<WeatherServiceSettings> settings)
        {
            _httpClient = httpClient;
            _apiKey = settings.Value.ApiKey;
            _baseUrl = settings.Value.BaseUrl;
        }

        public async Task<int?> GetCityIdByNameAsync(string cityName)
        {
            // logic to get the city ID by name
            var response = await _httpClient.GetAsync($"{_baseUrl}?q={cityName}&appid={_apiKey}");
            if (!response.IsSuccessStatusCode) return null;

            var weatherData = await response.Content.ReadFromJsonAsync<CityResponse>();
            return weatherData?.Id;
        }

        public async Task<object> GetWeatherByCityIdAsync(int cityId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}?id={cityId}&appid={_apiKey}");
            if (!response.IsSuccessStatusCode) return null;

            var weatherData = await response.Content.ReadFromJsonAsync<object>();
            return weatherData;
        }
    }
}
