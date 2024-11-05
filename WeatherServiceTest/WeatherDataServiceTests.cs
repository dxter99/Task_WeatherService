using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Controllers;
using WeatherService.Models;
using WeatherService.Services;

namespace WeatherServiceTest
{
    public class WeatherControllerTests
    {
        private Mock<IWeatherDataService> _mockWeatherService;
        private WeatherController _weatherController;
        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherDataService>();

            // Set up the mock methods to return expected values
            _mockWeatherService
                .Setup(service => service.GetCityIdByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(2643741); // Example return value for a city ID

            _weatherController = new WeatherController(_mockWeatherService.Object);
        }

        [Fact]
        public async Task FetchWeatherDataFromFile_ShouldReturnOk_WhenDataIsFetched()
        {
            // Arrange
            var testFilePath = @"C:\Users\Shubham\Downloads\Test_File.csv"; // Ensure this is a valid path

            // Act
            var result = await _weatherController.FetchWeatherDataFromFile(testFilePath);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
