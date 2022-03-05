namespace Weather.Service.WeatherProvider.WMO.DTO
{
    public class CityWeather
    {
        public Country Country { get; set; } = null!;
        public City City { get; set; } = null!;
        public Forecast? Forecast { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }

    public class Forecast
    {
        public DateTime DateIssued { get; set; }
        public string Timezone { get; set; } = string.Empty;
        public List<DailyForecast> DailyForecasts { get; set; } = new();
    }

    public class DailyForecast
    {
        public DateTime DateIssued { get; set; }
        public string WeatherDescription { get; set; } = string.Empty;
        public List<Temperature> Temperature { get; set; } = new();
        public WeatherIcon? WeatherIcon { get; set; }
    }

    public class Temperature
    {
        public string Unit { get; set; } = null!;
        public string MinValue { get; set; } = null!;
        public string MaxValue { get; set; } = null!;
    }

    public class WeatherIcon
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}