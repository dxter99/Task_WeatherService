namespace WeatherService.Models
{
    public class WeatherResponse
    {
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }
}
