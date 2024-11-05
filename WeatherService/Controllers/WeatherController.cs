using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WeatherService.Models;
using WeatherService.Services;

namespace WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherDataService _weatherService;

        public WeatherController(IWeatherDataService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("fetchFromFile")]
        public async Task<IActionResult> FetchWeatherDataFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest("The specified file does not exist.");
            }

            // Step 1: Read the city names from the file
            var cityNames = await System.IO.File.ReadAllTextAsync(filePath);
            var cities = cityNames.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
            Directory.CreateDirectory(outputDir);

            var failedCities = new List<string>();

            // Step 2: Process each city name
            foreach (var cityName in cities)
            {
                try
                {
                    var trimmedCityName = cityName.Trim();

                    // Step 3: Get the city ID using the city name
                    var cityId = await _weatherService.GetCityIdByNameAsync(trimmedCityName);
                    if (cityId == null)
                    {
                        failedCities.Add(trimmedCityName);
                        continue;
                    }

                    // Step 4: Fetch the weather data using the city ID
                    var weather = await _weatherService.GetWeatherByCityIdAsync(cityId.Value);

                    // Step 5: Save the weather data to a file
                    var fileName = $"{trimmedCityName}_{DateTime.Today:yyyyMMdd}.json";
                    var filePathOutput = Path.Combine(outputDir, fileName);
                    await System.IO.File.WriteAllTextAsync(filePathOutput, System.Text.Json.JsonSerializer.Serialize(weather));
                }
                catch (Exception ex)
                {
                    failedCities.Add(cityName); // Log the failed city
                    // You can add logging here to capture exceptions
                }
            }

            if (failedCities.Count > 0)
            {
                return Ok($"Weather data fetched and saved successfully, but some cities failed: {string.Join(", ", failedCities)}");
            }

            return Ok("Weather data fetched and saved successfully for all cities.");
        }
    }
}
