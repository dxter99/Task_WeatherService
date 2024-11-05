namespace WeatherService.Models
{
    public interface IWeatherDataService
    {
        Task<int?> GetCityIdByNameAsync(string cityName);
        Task<object> GetWeatherByCityIdAsync(int cityId);
    }
}
